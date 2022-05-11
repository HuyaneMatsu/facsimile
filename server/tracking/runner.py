__all__ = ('run',)

from time import perf_counter

from .connection import try_connect_socket
from .expression import detect_expressions
from .eye_openness import get_left_eye_openness, get_right_eye_openness
from .eyebrow_liftedness import get_eyebrow_liftedness
from .face_position import get_face_position
from .face_mesh_getter import iter_face_meshes
from .head import get_head_x_rotation, get_head_y_rotation, get_head_z_rotation
from .iris import accelerate_iris_right_x, accelerate_iris_left_x, get_left_iris_rotations, get_right_iris_rotations
from .mouth_openness import get_mouth_openness
from .smile import get_smile_ratio
from .smoothing import OneEuroSmoother1D
from .variables import SHOULD_CONNECT
from .constants import PACKET_TYPE_MOVEMENT, PACKET_TYPE_EXPRESSION


def run():
    iris_right_x = 0.0
    iris_right_y = 0.0
    iris_left_x = 0.0
    iris_left_y = 0.0
    
    eye_openness_left = 0.0
    eye_openness_right = 0.0
    
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
    
    
    happiness = 0.0
    sadness = 0.0
    surprise = 0.0
    fear = 0.0
    disgust = 0.0
    anger = 0.0
    
    time = perf_counter()
    
    
    iris_right_x_smoother = OneEuroSmoother1D(iris_right_x, time)
    iris_right_y_smoother = OneEuroSmoother1D(iris_right_y, time)
    iris_left_x_smoother = OneEuroSmoother1D(iris_left_x, time)
    iris_left_y_smoother = OneEuroSmoother1D(iris_left_y, time)
    
    eye_openness_left_smoother = OneEuroSmoother1D(eye_openness_left, time, acceleration=2.5)
    eye_openness_right_smoother = OneEuroSmoother1D(eye_openness_right, time, acceleration=2.5)
    
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
    
    
    happiness_smoother = OneEuroSmoother1D(happiness, time)
    sadness_smoother = OneEuroSmoother1D(sadness, time)
    surprise_smoother = OneEuroSmoother1D(surprise, time)
    fear_smoother = OneEuroSmoother1D(fear, time)
    disgust_smoother = OneEuroSmoother1D(disgust, time)
    anger_smoother = OneEuroSmoother1D(anger, time)
    
    while True:
        if SHOULD_CONNECT:
            socket = try_connect_socket()
        else:
            socket = None
        
        for face_landmarks, image in iter_face_meshes():
            landmarks = face_landmarks.landmark
            time = perf_counter()
            
            mouth_openness_x, mouth_openness_y = get_mouth_openness(landmarks)
            
            
            head_x = get_head_x_rotation(landmarks)
            head_y = get_head_y_rotation(landmarks)
            head_z = get_head_z_rotation(landmarks)
            
            face_position_x, face_position_y, face_position_z = get_face_position(landmarks)
            
            if head_x > 20.0:
                eye_openness_left = get_left_eye_openness(landmarks)
                eye_openness_right = eye_openness_left
                
                if eye_openness_left < 40.0:
                    iris_right_x = 0.0
                    iris_right_y = 0.0
                    iris_left_x = 0.0
                    iris_left_y = 0.0
                    
                else:
                    iris_ratio_right = get_right_iris_rotations(landmarks)
                    if (iris_ratio_right is not None):
                        iris_right_x, iris_right_y = iris_ratio_right
                    
                    iris_left_x = iris_right_x
                    iris_left_y = iris_right_y
            
            elif head_x < -20.0:

                eye_openness_right = get_right_eye_openness(landmarks)
                eye_openness_left = eye_openness_right
                
                if eye_openness_right < 40.0:
                    iris_right_x = 0.0
                    iris_right_y = 0.0
                    iris_left_x = 0.0
                    iris_left_y = 0.0
                
                else:
                    iris_ratio_left = get_left_iris_rotations(landmarks)
                    if (iris_ratio_left is not None):
                        iris_left_x, iris_left_y = iris_ratio_left
                    
                    iris_right_x = iris_left_x
                    iris_right_y = iris_left_y
                
            
            else:
                eye_openness_left = get_left_eye_openness(landmarks)
                eye_openness_right = get_right_eye_openness(landmarks)
                
                if eye_openness_left < 40.0:
                    iris_right_x = 0.0
                    iris_right_y = 0.0
                else:
                    iris_ratio_right = get_right_iris_rotations(landmarks)
                    if (iris_ratio_right is not None):
                        iris_right_x, iris_right_y = iris_ratio_right
                
                if eye_openness_right < 40.0:
                    iris_left_x = 0.0
                    iris_left_y = 0.0
                else:
                    iris_ratio_left = get_left_iris_rotations(landmarks)
                    if (iris_ratio_left is not None):
                        iris_left_x, iris_left_y = iris_ratio_left
                
                iris_left_y = iris_right_y = (iris_right_y + iris_left_y) * 0.5
            
            
            if head_y < -45.0:
                if iris_left_y > iris_right_y:
                    iris_right_y = iris_left_y
                else:
                    iris_left_y = iris_right_y
            
            elif head_y > 15.0:
                if iris_right_y > iris_left_y:
                    iris_right_y = iris_left_y
                else:
                    iris_left_y = iris_right_y
            
            # Expressions
            smile_ratio = get_smile_ratio(landmarks)
            eyebrow_liftedness = get_eyebrow_liftedness(landmarks)
            
            # Accelerate irises
            
            iris_right_x = accelerate_iris_right_x(iris_right_x)
            iris_left_x = accelerate_iris_left_x(iris_left_x)
            
            # Smooth movement
            
            # Change acceleration if required. Using no acceleration at lower changes makes it more smooth.
            if abs(head_x - head_x_smoother.previous_value) > 10.0:
                head_x_smoother.acceleration = 0.5
            else:
                head_x_smoother.acceleration = 0.0
            
            head_x = head_x_smoother(head_x, time)
            head_y = head_y_smoother(head_y, time)
            head_z = head_z_smoother(head_z, time)
            
            iris_right_x = iris_right_x_smoother(iris_right_x, time)
            iris_right_y = iris_right_y_smoother(iris_right_y, time)
            
            iris_left_x = iris_left_x_smoother(iris_left_x, time)
            iris_left_y = iris_left_y_smoother(iris_left_y, time)
            
            eye_openness_left = eye_openness_left_smoother(eye_openness_left, time)
            eye_openness_right = eye_openness_right_smoother(eye_openness_right, time)
            
            
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
                        f'{PACKET_TYPE_MOVEMENT} '
                        f'{iris_left_x:.4f} {iris_left_y:.4f} {iris_right_x:.4f} {iris_right_y:.4f} '
                        f'{eye_openness_left:.4f} {eye_openness_right:.4f} '
                        f'{head_x:.4f} {head_y:.4f} {head_z:.4f} '
                        f'{mouth_openness_x:.4f} {mouth_openness_y:.4f} '
                        f'{face_position_x:.4f} {face_position_y:.4f} {face_position_z:.4f} '
                        f'{smile_ratio:.4f} {eyebrow_liftedness:.4f}'
                    ).encode())
                except BrokenPipeError:
                    break
            
            happiness, sadness, surprise, fear, disgust, anger = detect_expressions(landmarks, image)
            
            happiness = happiness_smoother(happiness, time)
            sadness = sadness_smoother(sadness, time)
            surprise = surprise_smoother(surprise, time)
            fear = fear_smoother(fear, time)
            disgust = disgust_smoother(disgust, time)
            anger = anger_smoother(anger, time)
            
            
            if (socket is not None):
                try:
                    socket.send((
                        f'{PACKET_TYPE_EXPRESSION} '
                        f'{happiness:.4f} {sadness:.4f} {surprise:.4f} {fear:.4f} {disgust:.4f} {anger:.4f}'
                    ).encode())
                except BrokenPipeError:
                    break
        
        else:
            break
