const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
const exports = await getAssemblyExports("Sample.Browser.dll");

export async function hasPermission() {
    const status = await navigator.permissions.query({name: "microphone"});
    return status.state === "granted";
}

export async function requestPermission() {
    // getting the media stream makes the browser request for the permission
    const mediaStream = await navigator.mediaDevices.getUserMedia({audio: true});
    mediaStream.getTracks().forEach((track) => {
        if (track.readyState === 'live') {
            track.stop();
        }
    });
}

let mediaStream;
let mediaRecorder;


export async function startRecording() {
    mediaStream = await navigator.mediaDevices.getUserMedia({audio: true});
    mediaRecorder = new MediaRecorder(mediaStream);
    mediaRecorder.start();
    mediaRecorder.stream
    mediaRecorder.ondataavailable = (e) => {
        exports.Sample.Browser.JsMicrophone.OnAudioDataFromJs(e.data);
    };
}

export function stopRecording() {
    mediaStream.getTracks().forEach((track) => {
        if (track.readyState === 'live') {
            track.stop();
        }
    });
    mediaRecorder.stop();
}