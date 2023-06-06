__all__ = ('Vector3',)

import sys
from collections import namedtuple as NamedTuple
from contextlib import contextmanager as to_context_manager
from datetime import datetime as DateTime
from math import sqrt
from os import (
    close as close_file_descriptor, devnull, dup as duplicate_file_descriptor, dup2 as duplicate_file_descriptor_to
)

from .constants import DATETIME_FORMAT_CODE


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



@to_context_manager
def suppress_stdout_and_stderr(suppress_python):
    new_stdout = open(devnull, 'w')
    new_stderr = open(devnull, 'w')
    
    old_stdout_file_descriptor_original = sys.stdout.fileno()
    old_stderr_file_descriptor_original = sys.stderr.fileno()

    old_stdout_file_descriptor_duplicated = duplicate_file_descriptor(sys.stdout.fileno())
    old_stderr_file_descriptor_duplicated = duplicate_file_descriptor(sys.stderr.fileno())
    
    old_stdout = sys.stdout
    old_stderr = sys.stderr

    duplicate_file_descriptor_to(new_stdout.fileno(), old_stdout_file_descriptor_original)
    duplicate_file_descriptor_to(new_stderr.fileno(), old_stderr_file_descriptor_original)
    
    if suppress_python:
        sys.stdout = new_stdout
        sys.stderr = new_stderr

    yield
    
    if suppress_python:
        sys.stdout = old_stdout
        sys.stderr = old_stderr

    duplicate_file_descriptor_to(old_stdout_file_descriptor_duplicated, old_stdout_file_descriptor_original)
    duplicate_file_descriptor_to(old_stderr_file_descriptor_duplicated, old_stderr_file_descriptor_original)

    close_file_descriptor(old_stdout_file_descriptor_duplicated)
    close_file_descriptor(old_stderr_file_descriptor_duplicated)

    new_stdout.close()
    new_stderr.close()


def log(message, when = None):
    if when is None:
        when = DateTime.utcnow()
    
    sys.stdout.write(f'{when:{DATETIME_FORMAT_CODE}}: {message}\n')


def format_address(address):
    return f'{address[0]}:{address[1]}'
