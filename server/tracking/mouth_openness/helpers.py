__all__ = ()

from math import sqrt


def get_point_difference_3D(point_1, point_2):
    return sqrt(
        abs(point_1.x - point_2.x) ** 2 +
        abs(point_1.y - point_2.y) ** 2 +
        abs(point_1.z - point_2.z) ** 2
    )

# ~~ Unused ~~
# def get_point_difference_2D(point_1, point_2):
#     return sqrt(
#         abs(point_1.x - point_2.x) ** 2 +
#         abs(point_1.y - point_2.y) ** 2
#     )
