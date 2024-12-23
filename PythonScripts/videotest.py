from flask import Flask, Response
import time
import cv2

app = Flask(__name__)

# Video dosyalarının yolları
video_sources = {
    "1": r"C:\Users\kskn1\Desktop\1.mp4",
    "2": r"C:\Users\kskn1\Desktop\2.mp4",
    "3": r"C:\Users\kskn1\Desktop\3.mp4",
    "4": r"C:\Users\kskn1\Desktop\4.mp4"
}

# HOG + SVM person detection
hog = cv2.HOGDescriptor()
hog.setSVMDetector(cv2.HOGDescriptor_getDefaultPeopleDetector())

def detect_person(frame):
    """Kare içinde kişileri algılar ve çerçeve çizer."""
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    boxes, weights = hog.detectMultiScale(gray, winStride=(8, 8), padding=(8, 8), scale=1.05)
    for (x, y, w, h) in boxes:
        # Kişi algılanan alanı yeşil bir dikdörtgenle çizin
        cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
    return frame

def generate_frames(video_path):
    """Belirtilen video dosyasından kareler oluştur, kişileri algıla ve akış olarak gönder."""
    cap = cv2.VideoCapture(video_path)
    while cap.isOpened():
        success, frame = cap.read()
        if not success:
            cap.set(cv2.CAP_PROP_POS_FRAMES, 0)
            continue
        else:
            # Kişi algılama işlemi
            frame = detect_person(frame)
            _, buffer = cv2.imencode('.jpg', frame)
            frame = buffer.tobytes()
            yield (b'--frame\r\n'
                   b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n')
            time.sleep(0.05)

@app.route('/video_feed/<video_id>')
def video_feed(video_id):
    """Dinamik uç nokta: Video ID'ye göre kaynak yayınla."""
    video_path = video_sources.get(video_id)
    if not video_path:
        return "Video kaynağı bulunamadı!", 404
    return Response(generate_frames(video_path), mimetype='multipart/x-mixed-replace; boundary=frame')

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
