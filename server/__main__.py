import sys
from os import getcwd as get_current_work_directory
from os.path import (
    dirname as get_directory_name, expanduser as get_user_home_directory, join as join_paths,
    normpath as normalize_path, realpath as get_real_path, basename as get_base_name
)

try:
    from . import __package__ as PACKAGE_NAME
except ImportError:
    sys.path.append(
        normalize_path(
            join_paths(
                get_directory_name(
                    get_real_path(
                        join_paths(
                            get_current_work_directory(),
                            get_user_home_directory(__file__),
                        )
                    )
                ),
                '..',
            )
        )
    )
    
    PACKAGE_NAME = get_base_name(get_directory_name(__file__))


PACKAGE = __import__(PACKAGE_NAME)


def __main__():
    """
    Calls the connect - detect - send loop.
    """
    try:
        PACKAGE.run()
    except KeyboardInterrupt as err:
        raise SystemExit from err


if __name__ == '__main__':
    __main__()
