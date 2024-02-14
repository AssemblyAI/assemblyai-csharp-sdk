export function create(dotnetObjectReference) {
    let stream;
    let audioContext;
    let audioWorkletNode;
    let source;
    let audioBufferQueue = new Int16Array(0);
    return {
        async startRecording() {
            stream = await navigator.mediaDevices.getUserMedia({audio: true});
            audioContext = new AudioContext({
                sampleRate: 16_000,
                latencyHint: 'balanced'
            });
            source = audioContext.createMediaStreamSource(stream);
            
            await audioContext.audioWorklet.addModule('/_content/BlazorSample.Shared/audio-processor.js');
            audioWorkletNode = new AudioWorkletNode(audioContext, 'audio-processor');
            
            source.connect(audioWorkletNode);
            audioWorkletNode.connect(audioContext.destination);
            audioWorkletNode.port.onmessage = (event) => {
                const currentBuffer = new Int16Array(event.data.audio_data)
                audioBufferQueue = mergeBuffers(
                    audioBufferQueue,
                    currentBuffer
                )

                const bufferDuration =
                    (audioBufferQueue.length / audioContext.sampleRate) * 1000

                if (bufferDuration >= 100) {
                    const totalSamples = Math.floor(audioContext.sampleRate * 0.1)

                    const finalBuffer = new Uint8Array(
                        audioBufferQueue.subarray(0, totalSamples).buffer
                    )

                    audioBufferQueue = audioBufferQueue.subarray(totalSamples)
                    dotnetObjectReference.invokeMethodAsync("OnAudioDataFromJs", finalBuffer);
                }
            }
        },
        stopRecording() {
            stream?.getTracks().forEach((track) => track.stop());
            audioContext?.close();
            audioWorkletNode?.close();
            source?.close();
            audioBufferQueue = new Int16Array(0);
        }
    }
}
const mergeBuffers = (lhs, rhs) => {
    const mergedBuffer = new Int16Array(lhs.length + rhs.length)
    mergedBuffer.set(lhs, 0)
    mergedBuffer.set(rhs, lhs.length)
    return mergedBuffer
}