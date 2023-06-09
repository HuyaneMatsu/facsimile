__all__ = ('get_back_y_rotation',)

from math import atan2, sqrt, tau

from ....helpers import get_point_average_3d
from ....points.body import (
    POINT_BODY__HIP__LEFT, POINT_BODY__HIP__RIGHT, POINT_BODY__SHOULDER__LEFT, POINT_BODY__SHOULDER__RIGHT
)


def get_back_y_rotation(landmarks, rotation_x):
    shoulder = get_point_average_3d(landmarks[POINT_BODY__SHOULDER__RIGHT], landmarks[POINT_BODY__SHOULDER__LEFT])
    hip = get_point_average_3d(landmarks[POINT_BODY__HIP__RIGHT], landmarks[POINT_BODY__HIP__LEFT])
    
    height = abs(shoulder.y - hip.y)
    length = sqrt((shoulder.x - hip.x) ** 2 + (shoulder.z - hip.z) ** 2)
    
    value = (atan2(length, height) * 360.0 / tau)
    
    # If we are looking forward, the rotation breaks, so we will reduce the value
    rotation_x = min(abs(rotation_x), 90)
    value -= (20.0 / 90.0) * (90.0 - rotation_x)
    value = max(value, 0.0)
    return value
