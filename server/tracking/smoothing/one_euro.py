__all__ = ('OneEuroSmoother1D',)

from math import tau


def smoothing_factor(time_difference, cutoff):
    r = tau * cutoff * time_difference
    return r / (r + 1)


def exponential_smoothing(alpha, value, previous_value):
    return alpha * value + (1 - alpha) * previous_value


class OneEuroSmoother1D:
    __slots__ = (
        'min_cutoff', 'acceleration', 'cutoff_for_derivative', 'previous_value', 'previous_derivative_value',
        'previous_time',
    )
    
    def __new__(
        cls,
        initial_value,
        initial_time,
        derivative_initial_value = 0.0,
        min_cutoff = 1.0,
        acceleration = 0.0,
        cutoff_for_derivative = 1.0,
    ):
        self = object.__new__(cls)
        
        self.min_cutoff = min_cutoff
        self.acceleration = acceleration
        self.cutoff_for_derivative = cutoff_for_derivative
        
        self.previous_value = initial_value
        self.previous_derivative_value = derivative_initial_value
        self.previous_time = initial_time
        
        return self
    
    
    def __call__(self, value, time):
        time_difference = time - self.previous_time

        # derivative
        derivative_alpha = smoothing_factor(time_difference, self.cutoff_for_derivative)
        derivative_value = (value - self.previous_value) / time_difference
        new_derivative_value = exponential_smoothing(derivative_alpha, derivative_value, self.previous_derivative_value)

        # value
        cutoff = self.min_cutoff + self.acceleration * abs(new_derivative_value)
        alpha = smoothing_factor(time_difference, cutoff)
        new_value = exponential_smoothing(alpha, value, self.previous_value)

        self.previous_value = new_value
        self.previous_derivative_value = new_derivative_value
        self.previous_time = time

        return new_value
