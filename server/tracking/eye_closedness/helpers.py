__all__ = ()

from ..helpers import get_point_difference_3d

from .constants import EYE_OPEN_MULTIPLIER, MAX_EYE_CLOSEDNESS, MIN_EYE_CLOSEDNESS


def get_eye_closedness(
    eye_outline__horizontal_1, eye_outline__horizontal_2, eye_outline__vertical_1, eye_outline__vertical_2
):
    horizontal_difference = get_point_difference_3d(eye_outline__horizontal_1, eye_outline__horizontal_2)
    vertical_difference = get_point_difference_3d(eye_outline__vertical_1, eye_outline__vertical_2)
    
    if vertical_difference * EYE_OPEN_MULTIPLIER > horizontal_difference:
        eye_closedness = MIN_EYE_CLOSEDNESS
    else:
        eye_closedness = MAX_EYE_CLOSEDNESS
    
    return eye_closedness
