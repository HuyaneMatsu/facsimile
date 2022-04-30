__all__ = ('iter_face_meshes',)

import sys

from cv2 import (
    CAP_PROP_FPS as CAPTURE_PROPERTY__FPS, VideoCapture, flip, imshow as show_image, waitKey as wait_for_key_press
)
import mediapipe

from .variables import SHOULD_DRAW


mp_face_mesh = mediapipe.solutions.face_mesh
mp_drawing = mediapipe.solutions.drawing_utils
mp_drawing_styles = mediapipe.solutions.drawing_styles


def iter_face_meshes():
    camera = VideoCapture(0)
    camera.set(CAPTURE_PROPERTY__FPS, 30)
    
    try:
        with mp_face_mesh.FaceMesh(
            max_num_faces = 1,
            refine_landmarks = True,
            min_detection_confidence = 0.5,
            min_tracking_confidence = 0.5,
        ) as face_mesh:
            while True:
                success, image = camera.read()
                
                if not success:
                    sys.stderr.write('Ignoring empty camera frame.')
                    continue
                
                image.flags.writeable = False
                results = face_mesh.process(image)
                
                multi_face_landmarks = results.multi_face_landmarks
                if (multi_face_landmarks is None):
                    face_landmarks = None
                else:
                    face_landmarks = multi_face_landmarks[0]
                    
                    yield face_landmarks
                
                if SHOULD_DRAW:
                    image.flags.writeable = True
                    

                    if (face_landmarks is not None):
                        mp_drawing.draw_landmarks(
                            image = image,
                            landmark_list = face_landmarks,
                            connections = mp_face_mesh.FACEMESH_TESSELATION,
                            landmark_drawing_spec = None,
                            connection_drawing_spec = mp_drawing_styles.get_default_face_mesh_tesselation_style()
                        )
                        
                        mp_drawing.draw_landmarks(
                            image = image,
                            landmark_list = face_landmarks,
                            connections = mp_face_mesh.FACEMESH_CONTOURS,
                            landmark_drawing_spec = None,
                            connection_drawing_spec = mp_drawing_styles.get_default_face_mesh_contours_style()
                        )
                        
                        mp_drawing.draw_landmarks(
                            image = image,
                            landmark_list = face_landmarks,
                            connections = mp_face_mesh.FACEMESH_IRISES,
                            landmark_drawing_spec = None,
                        )
                    
                    image = flip(image, 1)
                    show_image('Face Mesh', image)
                    
                    if wait_for_key_press(1) & 0xFF == b'q'[0]:
                        return
    
    finally:
        camera.release()
