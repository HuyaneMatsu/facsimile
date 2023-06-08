__all__ = ('BodyDataStepper',)

from ..constants import PACKET_TYPE_BODY_MOVEMENT
from ..detection import (
    get_arm_upper_left_x_rotation, get_arm_upper_left_z_rotation, get_arm_upper_right_x_rotation,
    get_arm_upper_right_z_rotation, get_shoulder_x_rotation, get_shoulder_z_rotation
)
from ..smoothing import OneEuroSmoother1D

from .base import BaseDataStepper


class BodyDataStepper(BaseDataStepper):
    """
    Body data stepper.
    
    Attributes
    ----------
    arm_upper_left_x : `float`
        Left upper arm `x` axis rotation. Its value can between `-180.0` and `+180.0`.
    arm_upper_left_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.arm_upper_left_x``.
    arm_upper_right_x : `float`
        Left upper arm `x` axis rotation. Its value can between `-180.0` and `+180.0`.
    arm_upper_right_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.arm_upper_right_x``.
    arm_upper_left_z : `float`
        Left upper arm `z` axis rotation. Its value can between `-180.0` and `+180.0`.
    arm_upper_left_z_smoother : ``OneEuroSmoother1D``
        Smoother for ``.arm_upper_left_z``.
    arm_upper_right_z : `float`
        Left upper arm `z` axis rotation. Its value can between `-180.0` and `+180.0`.
    arm_upper_right_z_smoother : ``OneEuroSmoother1D``
        Smoother for ``.arm_upper_right_z``.
    shoulder_x : `float`
        Shoulder rotation on the `x` axis. Its value can between `-180.0` and `+180.0`.
    shoulder_x_smoother : ``OneEuroSmoother1D``
        Smoother for ``.shoulder_x``.
    shoulder_z : `float`
        Shoulder rotation on the `z` axis. Its value can between `-180.0` and `+180.0`.
    shoulder_z_smoother : ``OneEuroSmoother1D``
        Smoother for ``.shoulder_z``.
    """
    __slots__ = (
        'arm_upper_left_x', 'arm_upper_left_x_smoother', 'arm_upper_right_x', 'arm_upper_right_x_smoother', 'arm_upper_left_z',
        'arm_upper_left_z_smoother', 'arm_upper_right_z', 'arm_upper_right_z_smoother', 'shoulder_x', 'shoulder_x_smoother',
        'shoulder_z', 'shoulder_z_smoother'
    )
    
    def __new__(cls, time):
        """
        Parameters
        ----------
        time : `float`
            Monotonic time.
        """
        self = object.__new__(cls)
        
        self.arm_upper_left_x = arm_upper_left_x = 0.0
        self.arm_upper_left_z = arm_upper_left_z = 0.0
        self.arm_upper_right_x = arm_upper_right_x = 0.0
        self.arm_upper_right_z = arm_upper_right_z = 0.0
        self.shoulder_x = shoulder_x = 0.0
        self.shoulder_z = shoulder_z = 0.0
        
        self.shoulder_x_smoother = OneEuroSmoother1D(shoulder_x, time, acceleration = 0.0)
        self.shoulder_z_smoother = OneEuroSmoother1D(shoulder_z, time, acceleration = 0.0)
        self.arm_upper_left_x_smoother = OneEuroSmoother1D(arm_upper_left_x, time, acceleration = 0.0)
        self.arm_upper_left_z_smoother = OneEuroSmoother1D(arm_upper_left_z, time, acceleration = 0.0)
        self.arm_upper_right_x_smoother = OneEuroSmoother1D(arm_upper_right_x, time, acceleration = 0.0)
        self.arm_upper_right_z_smoother = OneEuroSmoother1D(arm_upper_right_z, time, acceleration = 0.0)
        
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
        body_landmarks = landmarks.body
        if body_landmarks is None:
            return
        
        arm_upper_left_x = get_arm_upper_left_x_rotation(body_landmarks)
        arm_upper_left_z = get_arm_upper_left_z_rotation(body_landmarks)
        arm_upper_right_x = get_arm_upper_right_x_rotation(body_landmarks)
        arm_upper_right_z = get_arm_upper_right_z_rotation(body_landmarks)
        shoulder_x = get_shoulder_x_rotation(body_landmarks)
        shoulder_z = get_shoulder_z_rotation(body_landmarks)
        
        self.arm_upper_left_x = self.arm_upper_left_x_smoother(arm_upper_left_x, time)
        self.arm_upper_left_z = self.arm_upper_left_z_smoother(arm_upper_left_z, time)
        self.arm_upper_right_x = self.arm_upper_right_x_smoother(arm_upper_right_x, time)
        self.arm_upper_right_z = self.arm_upper_right_z_smoother(arm_upper_right_z, time)
        self.shoulder_x = self.shoulder_x_smoother(shoulder_x, time)
        self.shoulder_z = self.shoulder_z_smoother(shoulder_z, time)
    
    
    def get_data(self):
        """
        Gets data of the stepper for its current state.
        
        Returns
        -------
        data : `bytes`
        """
        return (
            f'{PACKET_TYPE_BODY_MOVEMENT} '
            f'{self.arm_upper_left_x:.4f} '
            f'{self.arm_upper_left_z:.4f} '
            f'{self.arm_upper_right_x:.4f} '
            f'{self.arm_upper_right_z:.4f} '
            f'{self.shoulder_x:.4f} '
            f'{self.shoulder_z:.4f}'
        ).encode()
