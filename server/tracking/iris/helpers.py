__all__ = ()

from .constants import EYE_RATIO_X_SHIFT_SCALING


def scale_iris_rotations(x_ratio, y_ratio):
    x_ratio *= 32.0
    
    if x_ratio > 8.0:
        x_ratio = 8.0
    
    elif x_ratio < -8.0:
        x_ratio = -8.0
    
    y_ratio *= -40.0
    if y_ratio > 10.0:
        y_ratio = 10.0
    elif y_ratio < -10.0:
        y_ratio = -10.0
    
    return x_ratio, y_ratio


def get_iris_rotations(
    iris__x__high, iris__x__low, iris__y__high, iris__y__low, eye_outline__x__high, eye_outline__x__low,
    eye_outline__y__high, eye_outline__y__low
):
    iris_radius_x = (iris__x__high - iris__x__low) * 0.5
    if iris_radius_x < 0.0:
        return None
    
    iris_radius_y = (iris__y__high - iris__y__low) * 0.5
    if iris_radius_y < 0.0:
        return None
    
    if eye_outline__x__high <= eye_outline__x__low:
        return None
    
    normalized_eye_outline__x__high = eye_outline__x__high - eye_outline__x__low
    normalized_iris__x__position = (iris__x__high + iris__x__low) * 0.5 - eye_outline__x__low
    
    if normalized_iris__x__position > normalized_eye_outline__x__high:
        return None
    
    if normalized_iris__x__position < 0.0:
        return None
    
    if normalized_eye_outline__x__high == 0.0:
        x_ratio = 0.0
    else:
        x_ratio = 0.5 + normalized_iris__x__position / -normalized_eye_outline__x__high * EYE_RATIO_X_SHIFT_SCALING
    
    
    normalized_eye_outline__y__high = eye_outline__y__high - eye_outline__y__low
    normalized_iris__y__position = (iris__y__high + iris__y__low) * 0.5 - eye_outline__y__low
    
    if normalized_eye_outline__y__high == 0.0:
        y_ratio = 0.0
    else:
        y_ratio = 0.5 + normalized_iris__y__position / -normalized_eye_outline__y__high
    
    
    return x_ratio, y_ratio
