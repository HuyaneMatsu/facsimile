__all__ = ('Vector3',)

from collections import namedtuple as NamedTuple
from math import sqrt


def get_point_difference_3d(point_1, point_2):
    return sqrt(
        (point_1.x - point_2.x) ** 2 +
        (point_1.y - point_2.y) ** 2 +
        (point_1.z - point_2.z) ** 2
    )


def get_point_average_2d(point_1, point_2):
    return Vector2(
        (point_1.x + point_2.x) * 0.5,
        (point_1.y + point_2.y) * 0.5,
    )


def get_point_average_3d(point_1, point_2):
    return Vector3(
        (point_1.x + point_2.x) * 0.5,
        (point_1.y + point_2.y) * 0.5,
        (point_1.z + point_2.z) * 0.5,
    )

def get_point_difference_2d(point_1, point_2):
    return sqrt(
        abs(point_1.x - point_2.x) ** 2 +
        abs(point_1.y - point_2.y) ** 2
    )


Vector2 = NamedTuple('Vector2', ('x', 'y'))
Vector3 = NamedTuple('Vector3', ('x', 'y', 'z'))
