__all__ = ('iter_face_meshes',)

from time import sleep

from .capture import CaptureThread
from .variables import SHOULD_DRAW

from cv2 import flip, imshow as show_image, waitKey as wait_for_key_press

import mediapipe
from .helpers import suppress_stdout_and_stderr

draw_landmarks = mediapipe.solutions.drawing_utils.draw_landmarks

FaceMesh = mediapipe.solutions.face_mesh.FaceMesh

FACE_MESH_CONNECTIONS_TESSELATION = mediapipe.solutions.face_mesh.FACEMESH_TESSELATION
FACE_MESH_CONNECTIONS_CONTOURS = mediapipe.solutions.face_mesh.FACEMESH_CONTOURS
FACE_MESH_CONNECTIONS_IRISES = mediapipe.solutions.face_mesh.FACEMESH_IRISES
FACE_MESH_STYLE_TESSELATION = mediapipe.solutions.drawing_styles.get_default_face_mesh_tesselation_style()
FACE_MESH_STYLE_CONTOURS = mediapipe.solutions.drawing_styles.get_default_face_mesh_contours_style()

with suppress_stdout_and_stderr(False):
    FACE_MESH_PROCESSOR = FaceMesh(
        max_num_faces = 1,
        refine_landmarks = True,
        min_detection_confidence = 0.5,
        min_tracking_confidence = 0.5,
    )
    
    # Some operations run async, so we need to sleep some to silence the stupid logging of theirs.
    sleep(1.0 / 15.0)


def iter_face_meshes():
    capture_thread = CaptureThread()

    try:
        capture_thread.read_frame()
        
        while True:
            frame = capture_thread.read_frame()
            frame.flags.writeable = False
            results = FACE_MESH_PROCESSOR.process(frame)
            
            multi_face_landmarks = results.multi_face_landmarks
            if (multi_face_landmarks is None):
                face_landmarks = None
            else:
                face_landmarks = multi_face_landmarks[0]
                
                yield face_landmarks, frame
            
            if SHOULD_DRAW:
                frame.flags.writeable = True
                
                if (face_landmarks is not None):
                    draw_landmarks(
                        image = frame,
                        landmark_list = face_landmarks,
                        connections = FACE_MESH_CONNECTIONS_TESSELATION,
                        landmark_drawing_spec = None,
                        connection_drawing_spec = FACE_MESH_STYLE_TESSELATION,
                    )
                    
                    draw_landmarks(
                        image = frame,
                        landmark_list = face_landmarks,
                        connections = FACE_MESH_CONNECTIONS_CONTOURS,
                        landmark_drawing_spec = None,
                        connection_drawing_spec = FACE_MESH_STYLE_CONTOURS,
                    )
                    
                    draw_landmarks(
                        image = frame,
                        landmark_list = face_landmarks,
                        connections = FACE_MESH_CONNECTIONS_IRISES,
                        landmark_drawing_spec = None,
                    )
                
                frame = flip(frame, 1)
                show_image('Face Mesh', frame)
                
                if wait_for_key_press(1) & 0xFF == b'q'[0]:
                    return
            
            continue
    
    finally:
        capture_thread.cancel()
        capture_thread.join()
