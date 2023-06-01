__all__ = ('iter_landmarks',)

from time import sleep

import mediapipe
from cv2 import flip, imshow as show_image, waitKey as wait_for_key_press

from .capture import CaptureThread
from .helpers import suppress_stdout_and_stderr
from .landmarks import Landmarks
from .variables import SHOULD_DRAW


draw_landmarks = mediapipe.solutions.drawing_utils.draw_landmarks

FaceMesh = mediapipe.solutions.face_mesh.FaceMesh
FullMesh = mediapipe.solutions.holistic.Holistic


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
    
    
    FULL_MESH_PROCESSOR = FullMesh(
        # max_num_faces = 1,
        # refine_landmarks = True,
        min_detection_confidence = 0.5,
        min_tracking_confidence = 0.5,
    )
    
    # Some operations run async, so we need to sleep some to silence the stupid logging of theirs.
    sleep(1.0 / 15.0)


def read_landmarks(frame):
    """
    Reads the landmarks from the given frame.
    
    Parameters
    ----------
    frame : `numpy.ndarray`
        Input frame.
    
    Returns
    -------
    landmarks : ``Landmarks``
    """
    result = FACE_MESH_PROCESSOR.process(frame)
    return Landmarks.from_face_mesh_result(frame, result)


def iter_landmarks():
    """
    Iterates over landmarks results.
    
    This function is an iterable generator.
    
    Yields
    ------
    landmarks : ``Landmarks``
    """
    capture_thread = CaptureThread()

    try:
        capture_thread.read_frame()
        
        while True:
            frame = capture_thread.read_frame()
            frame.flags.writeable = False
            
            landmarks = read_landmarks(frame)
            yield landmarks
            
            if SHOULD_DRAW:
                frame.flags.writeable = True
                
                face_for_drawing = landmarks.get_face_for_drawing()
                if (face_for_drawing is not None):
                    draw_landmarks(
                        image = frame,
                        landmark_list = face_for_drawing,
                        connections = FACE_MESH_CONNECTIONS_TESSELATION,
                        landmark_drawing_spec = None,
                        connection_drawing_spec = FACE_MESH_STYLE_TESSELATION,
                    )
                    
                    draw_landmarks(
                        image = frame,
                        landmark_list = face_for_drawing,
                        connections = FACE_MESH_CONNECTIONS_CONTOURS,
                        landmark_drawing_spec = None,
                        connection_drawing_spec = FACE_MESH_STYLE_CONTOURS,
                    )
                    
                    draw_landmarks(
                        image = frame,
                        landmark_list = face_for_drawing,
                        connections = FACE_MESH_CONNECTIONS_IRISES,
                        landmark_drawing_spec = None,
                    )
                
                frame = flip(frame, 1)
                show_image('Face Mesh', frame)
                
                if wait_for_key_press(1) & 0xff == b'q'[0]:
                    return
            
            continue
    
    finally:
        capture_thread.cancel()
        capture_thread.join()
