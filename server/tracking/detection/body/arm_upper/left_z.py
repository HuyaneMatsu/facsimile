__all__ = ('get_arm_upper_left_z_rotation',)

from math import atan2, tau

from ....points.body import POINT_BODY__ELBOW__LEFT, POINT_BODY__SHOULDER__LEFT


def get_arm_upper_left_z_rotation(landmarks):
    shoulder = landmarks[POINT_BODY__SHOULDER__LEFT]
    elbow = landmarks[POINT_BODY__ELBOW__LEFT]
    
    return (atan2(elbow.y - shoulder.y, elbow.x - shoulder.x) * 360.0 / tau)
