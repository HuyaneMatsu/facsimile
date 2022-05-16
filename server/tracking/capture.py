import sys
from datetime import datetime
from threading import Thread, Event as SyncEvent

from .constants import DATETIME_FORMAT_CODE, NOTIFY_INTERVAL, RETRY_AFTER
from .variables import CONNECT_URL

from cv2 import (
    CAP_PROP_FPS as CAPTURE_PROPERTY__FPS, VideoCapture, CAP_PROP_FRAME_WIDTH as CAPTURE_PROPERTY__FRAME_WIDTH,
    CAP_PROP_FRAME_HEIGHT as CAPTURE_PROPERTY__FRAME_HEIGHT
)



class CaptureThread(Thread):
    __slots__ = ('_stopping', '_frame_waiter', '_frame', '_last_notify', '_retry_after_event')
    
    def __init__(self):
        self._stopping = False
        self._frame = None
        self._frame_waiter = None
        self._last_notify = None
        self._retry_after_event = None
        Thread.__init__(self, daemon=True)
        self.start()
    
    
    def run(self):
        if CONNECT_URL is None:
            camera = VideoCapture(0)
        else:
            camera = VideoCapture(CONNECT_URL)
        
        try:
            camera.set(CAPTURE_PROPERTY__FPS, 30)
            camera.set(CAPTURE_PROPERTY__FRAME_WIDTH, 720)
            camera.set(CAPTURE_PROPERTY__FRAME_HEIGHT, 480)
            
            
            while True:
                if self._stopping:
                    return
                
                success, frame = camera.read()
                if success:
                    self._feed_frame(frame)
                else:
                    self._maybe_notify()
                    self._sleep()
        
        finally:
            camera.release()
    
    
    def _feed_frame(self, frame):
        self._frame = frame
        
        frame_waiter = self._frame_waiter
        if (frame_waiter is not None):
            self._frame_waiter = None
            frame_waiter.set()
        
    
    def read_frame(self):
        while True:
            frame = self._frame
            if (frame is None):
                frame_waiter = self._frame_waiter
                if (frame_waiter is None):
                    frame_waiter = SyncEvent()
                    self._frame_waiter = frame_waiter
                
                frame_waiter.wait()
            else:
                break
        
        self._frame = None
        return frame
    
    
    def cancel(self):
        if self._stopping:
            return
        
        self._stopping = True
        
        retry_after_event = self._retry_after_event
        if (retry_after_event is not None):
            retry_after_event.set()
    
    
    def _maybe_notify(self):
        now = datetime.utcnow()
        last_notify = self._last_notify
        if (last_notify is None) or (last_notify + NOTIFY_INTERVAL < now):
            self._last_notify = now
            sys.stderr.write(f'{now:{DATETIME_FORMAT_CODE}}Failed to read frame\n')
    
    
    def _sleep(self):
        retry_after_event = self._retry_after_event
        if (retry_after_event is None):
            retry_after_event = SyncEvent()
            self._retry_after_event = retry_after_event
        
        try:
            retry_after_event.wait(RETRY_AFTER)
        finally:
            self._retry_after_event = retry_after_event
