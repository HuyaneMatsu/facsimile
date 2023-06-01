__all__ = ('ExpressionDataStepper',)

from ..constants import PACKET_TYPE_EXPRESSION
from ..detection import detect_expressions
from ..smoothing import OneEuroSmoother1D

from .base import BaseDataStepper


class ExpressionDataStepper(BaseDataStepper):
    """
    Expression data stepper.
    
    Attributes
    ----------
    anger : `float`
        The detected anger value. Its value can be between `0.0` and `+100.0`.
    anger_smoother : ``OneEuroSmoother1D``
        Smoother for ``.anger``.
    disgust : `float`
        The detected disgust value. Its value can be between `0.0` and `+100.0`.
    disgust_smoother : ``OneEuroSmoother1D``
        Smoother for ``.disgust``.
    fear : `float`
        The detected fear value. Its value can be between `0.0` and `+100.0`.
    fear_smoother : ``OneEuroSmoother1D``
        Smoother for ``.fear``.
    happiness : `float`
        The detected happiness value. Its value can be between `0.0` and `+100.0`.
    happiness_smoother : ``OneEuroSmoother1D``
        Smoother for ``.happiness``.
    sadness : `float`
        The detected sadness value. Its value can be between `0.0` and `+100.0`.
    sadness_smoother : ``OneEuroSmoother1D``
        Smoother for ``.sadness``.
    surprise : `float`
        The detected surprise value. Its value can be between `0.0` and `+100.0`.
    surprise_smoother : ``OneEuroSmoother1D``
        Smoother for ``.surprise``.
    """
    __slots__ = (
        'anger', 'anger_smoother', 'disgust', 'disgust_smoother', 'fear', 'fear_smoother', 'happiness',
        'happiness_smoother', 'sadness', 'sadness_smoother', 'surprise', 'surprise_smoother'
    )
    
    def __new__(cls, time):
        """
        Parameters
        ----------
        time : `float`
            Monotonic time.
        """
        self = object.__new__(cls)
            
        self.happiness = happiness = 0.0
        self.sadness = sadness = 0.0
        self.surprise = surprise = 0.0
        self.fear = fear = 0.0
        self.disgust = disgust = 0.0
        self.anger = anger = 0.0
            
        self.happiness_smoother = OneEuroSmoother1D(happiness, time)
        self.sadness_smoother = OneEuroSmoother1D(sadness, time)
        self.surprise_smoother = OneEuroSmoother1D(surprise, time)
        self.fear_smoother = OneEuroSmoother1D(fear, time)
        self.disgust_smoother = OneEuroSmoother1D(disgust, time)
        self.anger_smoother = OneEuroSmoother1D(anger, time)
        
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
        
        happiness, sadness, surprise, fear, disgust, anger = detect_expressions(face_landmarks, landmarks.image)
            
        self.happiness = self.happiness_smoother(happiness, time)
        self.sadness = self.sadness_smoother(sadness, time)
        self.surprise = self.surprise_smoother(surprise, time)
        self.fear = self.fear_smoother(fear, time)
        self.disgust = self.disgust_smoother(disgust, time)
        self.anger = self.anger_smoother(anger, time)
        

    def get_data(self):
        """
        Gets data of the stepper for its current state.
        
        Returns
        -------
        data : `bytes`
        """
        return (
            f'{PACKET_TYPE_EXPRESSION} '
            f'{self.happiness:.4f} '
            f'{self.sadness:.4f} '
            f'{self.surprise:.4f} '
            f'{self.fear:.4f} '
            f'{self.disgust:.4f} '
            f'{self.anger:.4f}'
        ).encode()
