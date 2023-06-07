__all__ = ('Landmarks',)


class Landmarks:
    """
    Collection of read landmarks.
    
    Attributes
    ----------
    body : `None`, `object`
        Body landmarks.
    face : `None`, `google.protobuf.pyext._message.RepeatedCompositeCo`
        Face landmarks.
    hand_left : `None`, `object`
        Left hand landmarks.
    hand_right : `None`, `object`
        Right hand landmarks.
    image : `numpy.ndarray`
        The read image.
    result : `type<mediapipe.python.solution_base.SolutionOutputs>`
        The read result.
    """
    __slots__ = ('body', 'face', 'hand_left', 'hand_right', 'image', 'result')
    
    def __new__(cls, image, result, body, face, hand_left, hand_right):
        """
        Creates a new landmarks instance.
        
        Parameters
        ----------
        image : `numpy.ndarray`
            The read image.
        result : `type<mediapipe.python.solution_base.SolutionOutputs>`
            The read result.
        body : `None`, `object`
            Body landmarks.
        face : `None`, `google.protobuf.pyext._message.RepeatedCompositeCo`
            Face landmarks.
        hand_left : `None`, `object`
            Left hand landmarks.
        hand_right : `None`, `object`
            Right hand landmarks.
        """
        self = object.__new__(cls)
        self.result = result
        self.image = image
        self.body = body
        self.face = face
        self.hand_left = hand_left
        self.hand_right = hand_right
        return self
    
    
    @classmethod
    def from_face_mesh_result(cls, image, result):
        """
        Creates a new landmarks instance from the given image and face mesh result.
        
        Parameters
        ----------
        image : `numpy.ndarray`
            The read image.
        result : `type<mediapipe.python.solution_base.SolutionOutputs>`
            The read result.
        
        Returns
        -------
        self : `instance<cls>`
        """
        multi_face_landmarks = result.multi_face_landmarks
        if (multi_face_landmarks is None):
            face_landmarks = None
        else:
            face_landmarks = multi_face_landmarks[0].landmark
        
        return cls(image, result, None, face_landmarks, None, None)
    
    @classmethod
    def from_full_mesh_result(cls, image, result):
        """
        Creates a new landmarks instance from the given image and full mesh result.
        
        Parameters
        ----------
        image : `numpy.ndarray`
            The read image.
        result : `type<mediapipe.python.solution_base.SolutionOutputs>`
            The read result.
        
        Returns
        -------
        self : `instance<cls>`
        """
        face_landmarks = result.face_landmarks
        if (face_landmarks is not None):
            face_landmarks = face_landmarks.landmark
        
        body_landmarks = result.pose_landmarks
        if (body_landmarks is not None):
            body_landmarks = body_landmarks.landmark
        
        return cls(image, result, body_landmarks, face_landmarks, None, None)
    
    
    def get_face_for_drawing(self):
        """
        Gets face landmark information for drawing.
        
        Returns
        -------
        face_for_drawing : `mediapipe.framework.formats.landmark_pb2.NormalizedLandmarkList`
        """
        result = self.result
        if hasattr(result, 'face_landmarks'):
            return result.face_landmarks
        
        if hasattr(result, 'multi_face_landmarks'):
            return result.multi_face_landmarks[0]
    
    
    def get_body_for_drawing(self):
        """
        Gets body landmark information for drawing.
        
        Returns
        -------
        body_for_drawing : `mediapipe.framework.formats.landmark_pb2.NormalizedLandmarkList`
        """
        result = self.result
        if hasattr(result, 'pose_landmarks'):
            return result.pose_landmarks
