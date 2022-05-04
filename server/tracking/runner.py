__all__ = ('run',)

from time import perf_counter

from .connection import try_connect_socket
from .eye_closedness import get_left_eye_closedness, get_right_eye_closedness
from .eyebrow import get_eyebrow_liftedness
from .face_position import get_face_position
from .face_mesh_getter import iter_face_meshes
from .head import get_head_x_rotation, get_head_y_rotation, get_head_z_rotation
from .iris import accelerate_iris_left_x, accelerate_iris_right_x, get_left_iris_rotations, get_right_iris_rotations
from .mouth_openness import get_mouth_openness
from .smile import get_smile_ratio
from .smoothing import OneEuroSmoother1D
from .variables import SHOULD_CONNECT


def run():
    iris_left_x = 0.0
    iris_left_y = 0.0
    iris_right_x = 0.0
    iris_right_y = 0.0
    
    eye_closedness_left = 0.0
    eye_closedness_right = 0.0
    
    head_x = 0.0
    head_y = 0.0
    head_z = 0.0
    
    mouth_openness_x = 0.0
    mouth_openness_y = 0.0
    
    face_position_x = 0.0
    face_position_y = 0.0
    face_position_z = 0.0
    
    smile_ratio = 0.0
    eyebrow_liftedness = 0.0
    
    time = perf_counter()
    
    
    iris_left_x_smoother = OneEuroSmoother1D(iris_left_x, time)
    iris_left_y_smoother = OneEuroSmoother1D(iris_left_y, time)
    iris_right_x_smoother = OneEuroSmoother1D(iris_right_x, time)
    iris_right_y_smoother = OneEuroSmoother1D(iris_right_y, time)
    
    eye_closedness_left_smoother = OneEuroSmoother1D(eye_closedness_left, time, acceleration=2.5)
    eye_closedness_right_smoother = OneEuroSmoother1D(eye_closedness_right, time, acceleration=2.5)
    
    head_x_smoother = OneEuroSmoother1D(head_x, time, acceleration=0.0)
    head_y_smoother = OneEuroSmoother1D(head_y, time, acceleration=0.0)
    head_z_smoother = OneEuroSmoother1D(head_z, time, acceleration=0.0)
    
    
    mouth_openness_x_smoother = OneEuroSmoother1D(mouth_openness_x, time, acceleration=0.5)
    mouth_openness_y_smoother = OneEuroSmoother1D(mouth_openness_y, time, acceleration=0.5)
    
    face_position_x_smoother = OneEuroSmoother1D(face_position_x, time)
    face_position_y_smoother = OneEuroSmoother1D(face_position_y, time)
    face_position_z_smoother = OneEuroSmoother1D(face_position_z, time)
    
    smile_ratio_smoother = OneEuroSmoother1D(smile_ratio, time)
    eyebrow_liftedness_smoother = OneEuroSmoother1D(eyebrow_liftedness, time)
    
    while True:
        if SHOULD_CONNECT:
            socket = try_connect_socket()
        else:
            socket = None
        
        for face_landmarks in iter_face_meshes():
            landmarks = face_landmarks.landmark
            mouth_openness_x, mouth_openness_y = get_mouth_openness(landmarks)
            
            time = perf_counter()
            
            head_x = get_head_x_rotation(landmarks)
            head_y = get_head_y_rotation(landmarks)
            head_z = get_head_z_rotation(landmarks)
            
            face_position_x, face_position_y, face_position_z = get_face_position(landmarks)
            
            if head_x > 20.0:
                eye_closedness_left = get_left_eye_closedness(landmarks)
                eye_closedness_right = eye_closedness_left
                
                if eye_closedness_left:
                    iris_left_x = 0.0
                    iris_left_y = 0.0
                    iris_right_x = 0.0
                    iris_right_y = 0.0
                    
                else:
                    iris_ratio_left = get_right_iris_rotations(landmarks)
                    if (iris_ratio_left is not None):
                        iris_left_x, iris_left_y = iris_ratio_left
                    
                    iris_right_x = iris_left_x
                    iris_right_y = iris_left_y
            
            elif head_x < -20.0:

                eye_closedness_right = get_right_eye_closedness(landmarks)
                eye_closedness_left = eye_closedness_right
                
                if eye_closedness_right:
                    iris_left_x = 0.0
                    iris_left_y = 0.0
                    iris_right_x = 0.0
                    iris_right_y = 0.0
                
                else:
                    iris_ratio_right = get_left_iris_rotations(landmarks)
                    if (iris_ratio_right is not None):
                        iris_right_x, iris_right_y = iris_ratio_right
                    
                    iris_left_x = iris_right_x
                    iris_left_y = iris_right_y
                
            
            else:
                eye_closedness_left = get_left_eye_closedness(landmarks)
                eye_closedness_right = get_right_eye_closedness(landmarks)
                
                if eye_closedness_left:
                    iris_left_x = 0.0
                    iris_left_y = 0.0
                else:
                    iris_ratio_left = get_right_iris_rotations(landmarks)
                    if (iris_ratio_left is not None):
                        iris_left_x, iris_left_y = iris_ratio_left
                
                if eye_closedness_right:
                    iris_right_x = 0.0
                    iris_right_y = 0.0
                else:
                    iris_ratio_right = get_left_iris_rotations(landmarks)
                    if (iris_ratio_right is not None):
                        iris_right_x, iris_right_y = iris_ratio_right
            
            
            if head_y < -45.0:
                if iris_right_y > iris_left_y:
                    iris_left_y = iris_right_y
                else:
                    iris_right_y = iris_left_y
            
            elif head_y > 15.0:
                if iris_left_y > iris_right_y:
                    iris_left_y = iris_right_y
                else:
                    iris_right_y = iris_left_y
            
            # Expressions
            smile_ratio = get_smile_ratio(landmarks)
            eyebrow_liftedness = get_eyebrow_liftedness(landmarks)
            
            # Accelerate irises
            
            iris_left_x = accelerate_iris_left_x(iris_left_x)
            iris_right_x = accelerate_iris_right_x(iris_right_x)
            
            # Smooth movement
            
            # Change acceleration if required. Using no acceleration at lower changes makes it more smooth.
            if abs(head_x - head_x_smoother.previous_value) > 10.0:
                head_x_smoother.acceleration = 0.5
            else:
                head_x_smoother.acceleration = 0.0
            
            head_x = head_x_smoother(head_x, time)
            head_y = head_y_smoother(head_y, time)
            head_z = head_z_smoother(head_z, time)
            
            iris_left_x = iris_left_x_smoother(iris_left_x, time)
            iris_left_y = iris_left_y_smoother(iris_left_y, time)
            
            iris_right_x = iris_right_x_smoother(iris_right_x, time)
            iris_right_y = iris_right_y_smoother(iris_right_y, time)
            
            eye_closedness_left = eye_closedness_left_smoother(eye_closedness_left, time)
            eye_closedness_right = eye_closedness_right_smoother(eye_closedness_right, time)
            
            
            mouth_openness_x = mouth_openness_x_smoother(mouth_openness_x, time)
            mouth_openness_y = mouth_openness_y_smoother(mouth_openness_y, time)
            
            face_position_x = face_position_x_smoother(face_position_x, time)
            face_position_y = face_position_y_smoother(face_position_y, time)
            face_position_z = face_position_z_smoother(face_position_z, time)
            
            smile_ratio = smile_ratio_smoother(smile_ratio, time)
            eyebrow_liftedness = eyebrow_liftedness_smoother(eyebrow_liftedness, time)
            
            if (socket is not None):
                try:
                    socket.send((
                        f'{iris_left_x:.4f} {iris_left_y:.4f} {iris_right_x:.4f} {iris_right_y:.4f} '
                        f'{eye_closedness_left:.4f} {eye_closedness_right:.4f} '
                        f'{head_x:.4f} {head_y:.4f} {head_z:.4f} '
                        f'{mouth_openness_x:.4f} {mouth_openness_y:.4f} '
                        f'{face_position_x:.4f} {face_position_y:.4f} {face_position_z:.4f} '
                        f'{smile_ratio:.4f} {eyebrow_liftedness:.4f}'
                    ).encode())
                except BrokenPipeError:
                    break
        else:
            break
