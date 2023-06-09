__all__ = ('BaseDataStepper',)

from ..constants import PACKET_TYPE_NONE


class BaseDataStepper:
    """
    Base data stepper.
    """
    __slots__ = ()
    
    def __new__(cls, time):
        """
        Creates a new data stepper.
        
        Parameters
        ----------
        time : `float`
            Monotonic time.
        """
        raise NotImplementedError
    
    
    def step(self, time):
        """
        Steps.
        
        Parameters
        ----------
        time : `float`
            Monotonic time.
        landmarks : ``LandMarks``
            Object containing landmarks.
        
        Returns
        -------
        stepped : `bool`
        """
        raise NotImplementedError
    
    
    def get_data(self):
        """
        Gets data of the stepper for its current state.
        
        Returns
        -------
        data : `bytes`
        """
        return f'{PACKET_TYPE_NONE}'.encode()
