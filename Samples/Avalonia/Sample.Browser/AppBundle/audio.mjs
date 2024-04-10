const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
const exports = await getAssemblyExports("Sample.Browser.dll");

export async function hasPermission() {
    const status = await navigator.permissions.query({name: "microphone"});
    return status.state === "granted";
}

export async function requestPermission() {
    // getting the media stream makes the browser request for the permission
    mediaStream = await navigator.mediaDevices.getUserMedia({audio: true});
}

let mediaStream;
let mediaRecorder;


export async function startRecording() {
    if(!mediaStream) mediaStream = await navigator.mediaDevices.getUserMedia({audio: true});
    mediaRecorder = new MediaRecorder(mediaStream);
    mediaRecorder.ondataavailable = async (e) => {
        exports.Sample.Browser.JsMicrophone.OnAudioDataFromJs(new Uint8Array(await e.data.arrayBuffer()));
    };
    mediaRecorder.start(100); //100 ms per audio buffer
}

export function stopRecording() {
    mediaStream.getTracks().forEach((track) => {
        if (track.readyState === 'live') {
            track.stop();
        }
    });
    mediaRecorder.stop();
    mediaStream = null;
    mediaRecorder = null;
}