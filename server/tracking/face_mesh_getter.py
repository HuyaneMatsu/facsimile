__all__ = ('iter_face_meshes',)

from time import perf_counter

from cv2 import flip, imshow as show_image, waitKey as wait_for_key_press

import mediapipe

from .variables import SHOULD_DRAW
from .capture import CaptureThread

mp_face_mesh = mediapipe.solutions.face_mesh
mediapipe_drawing = mediapipe.solutions.drawing_utils
mediapipe_drawing_styles = mediapipe.solutions.drawing_styles


def iter_face_meshes():
    capture_thread = CaptureThread()
    
    try:
        with mp_face_mesh.FaceMesh(
            max_num_faces = 1,
            refine_landmarks = True,
            min_detection_confidence = 0.5,
            min_tracking_confidence = 0.5,
        ) as face_mesh:
            while True:
                
                frame = capture_thread.read_frame()
                
                frame.flags.writeable = False
                results = face_mesh.process(frame)
                
                multi_face_landmarks = results.multi_face_landmarks
                if (multi_face_landmarks is None):
                    face_landmarks = None
                else:
                    face_landmarks = multi_face_landmarks[0]
                    
                    yield face_landmarks, frame
                
                if SHOULD_DRAW:
                    frame.flags.writeable = True
                    
                    if (face_landmarks is not None):
                        mediapipe_drawing.draw_landmarks(
                            image = frame,
                            landmark_list = face_landmarks,
                            connections = mp_face_mesh.FACEMESH_TESSELATION,
                            landmark_drawing_spec = None,
                            connection_drawing_spec = mediapipe_drawing_styles.get_default_face_mesh_tesselation_style()
                        )
                        
                        mediapipe_drawing.draw_landmarks(
                            image = frame,
                            landmark_list = face_landmarks,
                            connections = mp_face_mesh.FACEMESH_CONTOURS,
                            landmark_drawing_spec = None,
                            connection_drawing_spec = mediapipe_drawing_styles.get_default_face_mesh_contours_style()
                        )
                        
                        mediapipe_drawing.draw_landmarks(
                            image = frame,
                            landmark_list = face_landmarks,
                            connections = mp_face_mesh.FACEMESH_IRISES,
                            landmark_drawing_spec = None,
                        )
                    
                    frame = flip(frame, 1)
                    show_image('Face Mesh', frame)
                    
                    if wait_for_key_press(1) & 0xFF == b'q'[0]:
                        return
    
    finally:
        capture_thread.cancel()
        capture_thread.join()
