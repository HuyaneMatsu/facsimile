__all__ = ()

from ..helpers import get_point_difference_3d

from .constants import EYE_OPENNESS_MULTIPLIER, EYE_OPENNESS_MAX, EYE_OPENNESS_MIN, EYE_OPENNESS_REDUCTION


def get_eye_openness(
    eye_outline__horizontal_1, eye_outline__horizontal_2, eye_outline__vertical_1, eye_outline__vertical_2
):
    horizontal_difference = get_point_difference_3d(eye_outline__horizontal_1, eye_outline__horizontal_2)
    vertical_difference = get_point_difference_3d(eye_outline__vertical_1, eye_outline__vertical_2)
    
    eye_openness = vertical_difference / horizontal_difference
    
    eye_openness = (eye_openness - EYE_OPENNESS_REDUCTION) * EYE_OPENNESS_MULTIPLIER
    
    if eye_openness > EYE_OPENNESS_MAX:
        eye_openness = EYE_OPENNESS_MAX
    elif eye_openness < EYE_OPENNESS_MIN:
        eye_openness = EYE_OPENNESS_MIN
    
    return eye_openness
