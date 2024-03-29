__all__ = ()

from datetime import timedelta

ADDRESS = ('127.0.0.1', 5050)

DATETIME_FORMAT_CODE = '%Y-%m-%d %H:%M:%S'

NOTIFY_INTERVAL = timedelta(minutes = 1)
RETRY_AFTER = 0.4


PACKET_TYPE_NONE = 0
PACKET_TYPE_HEAD_MOVEMENT = 1
PACKET_TYPE_EXPRESSION = 2
PACKET_TYPE_BODY_MOVEMENT = 3
PACKET_TYPE_HAND_MOVEMENT = 4
