__all__ = ('FaceDataStepper',)

from ..constants import PACKET_TYPE_HEAD_MOVEMENT
from ..detection import (
    accelerate_iris_left_x, accelerate_iris_right_x, get_eyebrow_liftedness, get_face_position, get_head_x_rotation,
    get_head_y_rotation, get_head_z_rotation, get_left_eye_openness, get_left_iris_rotations, get_mouth_openness,
    get_right_eye_openness, get_right_iris_rotations, get_smile_ratio
)
from ..smoothing import OneEuroSmoother1D

from .base import BaseDataStepper


class FaceDataStepper(BaseDataStepper):
    """
    Face data stepper.
    
    Attributes
    ----------
    eye_openness_left : `float`
        The openness of the left eye. Its value can be between `0.0` and `+100.0`.
    eye_openness_left_smoother : ``OneEuroSmoother1D``
        Smoother for ``.eye_openness_left``.
    eye_openness_right : `float`
        The openness of the right eye. Its value can be between `0.0` and `+100.0`.
    eye_openness_right_smoother : ``OneEuroSmoother1D``
        Smoother for ``.eye_openness_right``.
    eyebrow_liftedness : `float`
        How much the eyebrows are lifted. Its value can be between `0.0` and `+100.0`.
    eyebrow_liftedness_smoother : ``OneEuroSmoother1D``
        Smoother for ``.eyebrow_liftedness``.
    face_position_x : `float`
        Estimated face position in `x` axis. Its value can be between `-5.0` and `+5.0`.
    face_position_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.face_position_x``
    face_position_y : `float`
        Estimated face position in `y` axis. Its value can be between `-5.0` and `+5.0`.
    face_position_y_smoother : ``OneEuroSmoother1D``
        Smoother for ``.face_position_y``
    face_position_z : `float`
        Estimated face position in `z` axis. Its value can be between `-10.0` and `-1.0`.
    face_position_z_smoother : ``OneEuroSmoother1D``
        Smoother for ``.face_position_z``
    head_x : `float`
        Head rotation on the `x` axis. Its value can between `-90.0` and `+90.0`.
    head_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.head_x``.
    head_y : `float`
        Head rotation on the `y` axis. Its value can between `-60.0` and `+30.0`.
    head_y_smoother : ``OneEuroSmoother1D``
        Smoother for ``.head_y``.
    head_z : `float`
        Head rotation on the `z` axis. Its value can between `-90.0` and `+90.0`.
    head_z_smoother : ``OneEuroSmoother1D``
        Smoother for ``.head_z``.
    iris_left_x : `float`
        The left iris' position on the `x` axis. Its value can be between `-16.0` and `+8.0`.
    iris_left_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.iris_left_x``.
    iris_left_y : `float`
        The left iris' position on the `y` axis. Its value can be between `-10.0` and `-10.0`.
    iris_left_y_smoother : ``OneEuroSmoother1D``
        Smoother for ``.iris_left_y``.
    iris_right_x : `float`
        The right iris' position on the `x` axis. Its value can be between `-8.0` and `+16.0`.
    iris_right_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.iris_right_x``.
    iris_right_y : `float`
        The right iris' position on the `y` axis. Its value can be between `-10.0` and `-10.0`.
    iris_right_y_smoother : ``OneEuroSmoother1D``
        Smoother for ``.iris_right_y``.
    mouth_openness_x : `float`
        The mouth's openness on the `x` axis. Its value can be between `0.0` and `+100.0`.
    mouth_openness_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.mouth_openness_x``.
    mouth_openness_y : `float`
        The mouth's openness on the `y` axis. Its value can be between `0.0` and `+100.0`.
    mouth_openness_y_smoother : ``OneEuroSmoother1D``
        Smoother for ``.mouth_openness_y``.
    smile_ratio : `float`
        How big the smile is of the mouth. Its value can be between `0.0` and `+100.0`.
    smile_ratio_smoother : ``OneEuroSmoother1D``
        Smoother for ``.smile_ratio``.
    """
    __slots__ = (
        'eye_openness_left', 'eye_openness_left_smoother', 'eye_openness_right', 'eye_openness_right_smoother',
        'eyebrow_liftedness', 'eyebrow_liftedness_smoother', 'face_position_x', 'face_position_x_smoother',
        'face_position_y', 'face_position_y_smoother', 'face_position_z', 'face_position_z_smoother', 'head_x',
        'head_x_smoother', 'head_y', 'head_y_smoother', 'head_z', 'head_z_smoother', 'iris_left_x',
        'iris_left_x_smoother', 'iris_left_y', 'iris_left_y_smoother', 'iris_right_x', 'iris_right_x_smoother',
        'iris_right_y', 'iris_right_y_smoother', 'mouth_openness_x', 'mouth_openness_x_smoother', 'mouth_openness_y',
        'mouth_openness_y_smoother', 'smile_ratio', 'smile_ratio_smoother'
    )
    
    def __new__(cls, time):
        """
        Parameters
        ----------
        time : `float`
            Monotonic time.
        """
        self = object.__new__(cls)
        
        self.iris_right_x = iris_right_x = 0.0
        self.iris_right_y = iris_right_y = 0.0
        self.iris_left_x = iris_left_x = 0.0
        self.iris_left_y = iris_left_y = 0.0
        
        self.eye_openness_left = eye_openness_left = 0.0
        self.eye_openness_right = eye_openness_right = 0.0
        
        self.head_x = head_x = 0.0
        self.head_y = head_y = 0.0
        self.head_z = head_z = 0.0
        
        self.mouth_openness_x = mouth_openness_x = 0.0
        self.mouth_openness_y = mouth_openness_y = 0.0
        
        self.face_position_x = face_position_x = 0.0
        self.face_position_y = face_position_y = 0.0
        self.face_position_z = face_position_z = 0.0
        
        self.smile_ratio = smile_ratio = 0.0
        self.eyebrow_liftedness = eyebrow_liftedness = 0.0
        
        self.iris_right_x_smoother = OneEuroSmoother1D(iris_right_x, time)
        self.iris_right_y_smoother = OneEuroSmoother1D(iris_right_y, time)
        self.iris_left_x_smoother = OneEuroSmoother1D(iris_left_x, time)
        self.iris_left_y_smoother = OneEuroSmoother1D(iris_left_y, time)
        
        self.eye_openness_left_smoother = OneEuroSmoother1D(eye_openness_left, time, acceleration = 2.5)
        self.eye_openness_right_smoother = OneEuroSmoother1D(eye_openness_right, time, acceleration = 2.5)
        
        self.head_x_smoother = OneEuroSmoother1D(head_x, time, acceleration = 0.0)
        self.head_y_smoother = OneEuroSmoother1D(head_y, time, acceleration = 0.0)
        self.head_z_smoother = OneEuroSmoother1D(head_z, time, acceleration = 0.0)
        
        
        self.mouth_openness_x_smoother = OneEuroSmoother1D(mouth_openness_x, time, acceleration = 0.5)
        self.mouth_openness_y_smoother = OneEuroSmoother1D(mouth_openness_y, time, acceleration = 0.5)
        
        self.face_position_x_smoother = OneEuroSmoother1D(face_position_x, time)
        self.face_position_y_smoother = OneEuroSmoother1D(face_position_y, time)
        self.face_position_z_smoother = OneEuroSmoother1D(face_position_z, time)
        
        self.smile_ratio_smoother = OneEuroSmoother1D(smile_ratio, time)
        self.eyebrow_liftedness_smoother = OneEuroSmoother1D(eyebrow_liftedness, time)
        
        return self
    
    
    def step(self, time, landmarks):
        """
        Steps.
        
        Parameters
        ----------
        time : `float`
            Monotonic time.
        landmarks : ``Landmarks``
            Object containing landmarks.
        """
        face_landmarks = landmarks.face
        if face_landmarks is None:
            return
        
        mouth_openness_x, mouth_openness_y = get_mouth_openness(face_landmarks)
        
        head_x = get_head_x_rotation(face_landmarks)
        head_y = get_head_y_rotation(face_landmarks)
        head_z = get_head_z_rotation(face_landmarks)
        
        face_position_x, face_position_y, face_position_z = get_face_position(face_landmarks)
        
        if head_x > 20.0:
            eye_openness_left = get_left_eye_openness(face_landmarks)
            eye_openness_right = eye_openness_left
            
            if eye_openness_left < 40.0:
                iris_right_x = 0.0
                iris_right_y = 0.0
                iris_left_x = 0.0
                iris_left_y = 0.0
                
            else:
                iris_ratio_right = get_right_iris_rotations(face_landmarks)
                if (iris_ratio_right is None):
                    iris_right_x = self.iris_right_x
                    iris_right_y = self.iris_right_y
                else:
                    iris_right_x, iris_right_y = iris_ratio_right
                
                iris_left_x = iris_right_x
                iris_left_y = iris_right_y
        
        elif head_x < -20.0:

            eye_openness_right = get_right_eye_openness(face_landmarks)
            eye_openness_left = eye_openness_right
            
            if eye_openness_right < 40.0:
                iris_right_x = 0.0
                iris_right_y = 0.0
                iris_left_x = 0.0
                iris_left_y = 0.0
            
            else:
                iris_ratio_left = get_left_iris_rotations(face_landmarks)
                if (iris_ratio_left is None):
                    iris_left_x = self.iris_left_x
                    iris_left_y = self.iris_left_y
                else:
                    iris_left_x, iris_left_y = iris_ratio_left
                
                iris_right_x = iris_left_x
                iris_right_y = iris_left_y
            
        
        else:
            eye_openness_left = get_left_eye_openness(face_landmarks)
            eye_openness_right = get_right_eye_openness(face_landmarks)
            
            if eye_openness_left < 40.0:
                iris_right_x = 0.0
                iris_right_y = 0.0
            else:
                iris_ratio_right = get_right_iris_rotations(face_landmarks)
                if (iris_ratio_right is None):
                    iris_right_x = self.iris_right_x
                    iris_right_y = self.iris_right_y
                else:
                    iris_right_x, iris_right_y = iris_ratio_right
            
            if eye_openness_right < 40.0:
                iris_left_x = 0.0
                iris_left_y = 0.0
            else:
                iris_ratio_left = get_left_iris_rotations(face_landmarks)
                if (iris_ratio_left is None):
                    iris_left_x = self.iris_left_x
                    iris_left_y = self.iris_left_y
                else:
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
        smile_ratio = get_smile_ratio(face_landmarks)
        eyebrow_liftedness = get_eyebrow_liftedness(face_landmarks)
        
        # Accelerate irises
        
        iris_right_x = accelerate_iris_right_x(iris_right_x)
        iris_left_x = accelerate_iris_left_x(iris_left_x)
        
        # Smooth movement
        
        head_x_smoother = self.head_x_smoother
        # Change acceleration if required. Using no acceleration at lower changes makes it more smooth.
        if abs(head_x - head_x_smoother.previous_value) > 10.0:
            head_x_smoother.acceleration = 0.5
        else:
            head_x_smoother.acceleration = 0.0
        
        
        self.head_x = head_x_smoother(head_x, time)
        self.head_y = self.head_y_smoother(head_y, time)
        self.head_z = self.head_z_smoother(head_z, time)
        
        self.iris_right_x = self.iris_right_x_smoother(iris_right_x, time)
        self.iris_right_y = self.iris_right_y_smoother(iris_right_y, time)
        
        self.iris_left_x = self.iris_left_x_smoother(iris_left_x, time)
        self.iris_left_y = self.iris_left_y_smoother(iris_left_y, time)
        
        self.eye_openness_left = self.eye_openness_left_smoother(eye_openness_left, time)
        self.eye_openness_right = self.eye_openness_right_smoother(eye_openness_right, time)
        
        self.mouth_openness_x = self.mouth_openness_x_smoother(mouth_openness_x, time)
        self.mouth_openness_y = self.mouth_openness_y_smoother(mouth_openness_y, time)
        
        self.face_position_x = self.face_position_x_smoother(face_position_x, time)
        self.face_position_y = self.face_position_y_smoother(face_position_y, time)
        self.face_position_z = self.face_position_z_smoother(face_position_z, time)
        
        self.smile_ratio = self.smile_ratio_smoother(smile_ratio, time)
        self.eyebrow_liftedness = self.eyebrow_liftedness_smoother(eyebrow_liftedness, time)
        

    def get_data(self):
        """
        Gets data of the stepper for its current state.
        
        Returns
        -------
        data : `bytes`
        """
        return (
            f'{PACKET_TYPE_HEAD_MOVEMENT} '
            f'{self.iris_left_x:.4f} '
            f'{self.iris_left_y:.4f} '
            f'{self.iris_right_x:.4f} '
            f'{self.iris_right_y:.4f} '
            f'{self.eye_openness_left:.4f} '
            f'{self.eye_openness_right:.4f} '
            f'{self.head_x:.4f} '
            f'{self.head_y:.4f} '
            f'{self.head_z:.4f} '
            f'{self.mouth_openness_x:.4f} '
            f'{self.mouth_openness_y:.4f} '
            f'{self.face_position_x:.4f} '
            f'{self.face_position_y:.4f} '
            f'{self.face_position_z:.4f} '
            f'{self.smile_ratio:.4f} '
            f'{self.eyebrow_liftedness:.4f}'
        ).encode()
