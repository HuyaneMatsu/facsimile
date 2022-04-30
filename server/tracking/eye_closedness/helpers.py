__all__ = ()

from math import sqrt

from .constants import EYE_OPEN_MULTIPLIER, MAX_EYE_CLOSEDNESS, MIN_EYE_CLOSEDNESS


def get_eye_closedness(
    eye_outline__horizontal_1, eye_outline__horizontal_2, eye_outline__vertical_1, eye_outline__vertical_2
):
    horizontal_difference_x = abs(eye_outline__horizontal_1.x - eye_outline__horizontal_2.x)
    horizontal_difference_y = abs(eye_outline__horizontal_1.y - eye_outline__horizontal_2.y)
    horizontal_difference_absolute = sqrt(
        horizontal_difference_x * horizontal_difference_x + horizontal_difference_y * horizontal_difference_y
    )

    vertical_difference_x = abs(eye_outline__vertical_1.x - eye_outline__vertical_2.x)
    vertical_difference_y = abs(eye_outline__vertical_1.y - eye_outline__vertical_2.y)
    vertical_difference_absolute = sqrt(
        vertical_difference_x * vertical_difference_x + vertical_difference_y * vertical_difference_y
    )
    
    if vertical_difference_absolute * EYE_OPEN_MULTIPLIER > horizontal_difference_absolute:
        eye_closedness = MIN_EYE_CLOSEDNESS
    else:
        eye_closedness = MAX_EYE_CLOSEDNESS
    
    return eye_closedness
