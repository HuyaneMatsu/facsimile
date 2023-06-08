__all__ = ('get_arm_upper_right_z_rotation',)

from math import atan2, tau

from ....points.body import POINT_BODY__ELBOW__RIGHT, POINT_BODY__SHOULDER__RIGHT


def get_arm_upper_right_z_rotation(landmarks):
    shoulder = landmarks[POINT_BODY__SHOULDER__RIGHT]
    elbow = landmarks[POINT_BODY__ELBOW__RIGHT]
    
    return (atan2(shoulder.y - elbow.y, shoulder.x - elbow.x) * -360.0 / tau)
