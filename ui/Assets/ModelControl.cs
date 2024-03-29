using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

using Debug = UnityEngine.Debug;
using f32 = System.Single;
using f64 = System.Double;
using i32 = System.Int32;
using u64 = System.UInt64;


enum FACE_MESH {
    ALL_NATURAL,
    ALL_ANGRY,
    ALL_FUN,
    ALL_JOY,
    ALL_SORROW,
    ALL_SURPRISED,

    EYEBROW_ANGRY,
    EYEBROW_FUN,
    EYEBROW_JOY,
    EYEBROW_SORROW,
    EYEBROW_SURPRISED,

    EYE_NATURAL,
    EYE_ANGRY,
    EYE_CLOSE,
    EYE_CLOSE_RIGHT,
    EYE_CLOSE_LEFT,
    EYE_FUN,
    EYE_JOY,
    EYE_JOY_RIGHT,
    EYE_JOY_LEFT,
    EYE_SORROW,
    EYE_SURPRISED,
    EYE_SPREAD,
    EYE_IRIS_HIDE,
    EYE_HIGHLIGHT_HIDE,

    MOUTH_CLOSE,
    MOUTH_UP,
    MOUTH_DOWN,
    MOUTH_ANGRY,
    MOUTH_SMALL,
    MOUTH_LARGE,
    MOUTH_NEUTRAL,
    MOUTH_FUN,
    MOUTH_JOY,
    MOUTH_SORROW,
    MOUTH_SURPRISED,
    MOUTH_FUNG,
    MOUTH_FUNG_RIGHT,
    MOUTH_FUNG_LEFT,
    MOUTH_A,
    MOUTH_I,
    MOUTH_U,
    MOUTH_E,
    MOUTH_O,

    TEETH_HIDE,
    TEETH_VAMPIRE,
    TEETH_VAMPIRE_BOT,
    TEETH_VAMPIRE_TOP,
    TEETH_SHARK,
    TEETH_SHARK_BOT,
    TEETH_SHARK_TOP,
    TEETH_MID_FANG,
    TEETH_MID_FANG_TOP,
    TEETH_MID_FANG_BOT,
    TEETH_SHORT,
    TEETH_SHORT_TOP,
    TEETH_SHORT_BOT
}


public static class BODY_PART_NAME {
    public const string HIPS = "J_Bip_C_Hips";
    public const string SPINE = "J_Bip_C_Spine";
    public const string CHEST = "J_Bip_C_Chest";
    public const string CHEST_UPPER = "J_Bip_C_UpperChest";
    public const string CHEST_LEFT_ROOT = "J_Sec_L_Bust1";
    public const string CHEST_LEFT_MID = "J_Sec_L_Bust2";
    public const string CHEST_RIGHT_ROOT = "J_Sec_R_Bust1";
    public const string CHEST_RIGHT_MID = "J_Sec_R_Bust2";
    public const string NECK = "J_Bip_C_Neck";
    public const string HEAD = "J_Bip_C_Head";
    public const string EYE_LEFT = "J_Adj_L_FaceEye";
    public const string EYE_RIGHT = "J_Adj_R_FaceEye";
    public const string CAT_EAR_LEFT_ROOT = "J_Opt_L_CatEar1_01";
    public const string CAT_EAR_LEFT_MID = "J_Opt_L_CatEar2_01";
    public const string CAT_EAR_RIGHT_ROOT = "J_Opt_R_CatEar1_01";
    public const string CAT_EAR_RIGHT_MID = "J_Opt_R_CatEar2_01";
    public const string SHOULDER_LEFT = "J_Bip_L_Shoulder";
    public const string ARM_UPPER_LEFT = "J_Bip_L_UpperArm";
    public const string ARM_LOWER_LEFT = "J_Bip_L_LowerArm";
    public const string HAND_LEFT = "J_Bip_L_Hand";
    public const string FINGER_INDEX_LEFT_ROOT = "J_Bip_L_Index1";
    public const string FINGER_INDEX_LEFT_MID = "J_Bip_L_Index2";
    public const string FINGER_INDEX_LEFT_END = "J_Bip_L_Index3";
    public const string FINGER_LITTLE_LEFT_ROOT = "J_Bip_L_Little1";
    public const string FINGER_LITTLE_LEFT_MID = "J_Bip_L_Little2";
    public const string FINGER_LITTLE_LEFT_END = "J_Bip_L_Little3";
    public const string FINGER_MIDDLE_LEFT_ROOT = "J_Bip_L_Middle1";
    public const string FINGER_MIDDLE_LEFT_MID = "J_Bip_L_Middle2";
    public const string FINGER_MIDDLE_LEFT_END = "J_Bip_L_Middle3";
    public const string FINGER_RING_LEFT_ROOT = "J_Bip_L_Ring1";
    public const string FINGER_RING_LEFT_MID = "J_Bip_L_Ring2";
    public const string FINGER_RING_LEFT_END = "J_Bip_L_Ring3";
    public const string FINGER_THUMB_LEFT_ROOT = "J_Bip_L_Thumb1";
    public const string FINGER_THUMB_LEFT_MID = "J_Bip_L_Thumb2";
    public const string FINGER_THUMB_LEFT_END = "J_Bip_L_Thumb3";
    public const string SHOULDER_RIGHT = "J_Bip_R_Shoulder";
    public const string ARM_UPPER_RIGHT = "J_Bip_R_UpperArm";
    public const string ARM_LOWER_RIGHT = "J_Bip_R_LowerArm";
    public const string HAND_RIGHT = "J_Bip_R_Hand";
    public const string FINGER_INDEX_RIGHT_ROOT = "J_Bip_R_Index1";
    public const string FINGER_INDEX_RIGHT_MID = "J_Bip_R_Index2";
    public const string FINGER_INDEX_RIGHT_END = "J_Bip_R_Index3";
    public const string FINGER_LITTLE_RIGHT_ROOT = "J_Bip_R_Little1";
    public const string FINGER_LITTLE_RIGHT_MID = "J_Bip_R_Little2";
    public const string FINGER_LITTLE_RIGHT_END = "J_Bip_R_Little3";
    public const string FINGER_MIDDLE_RIGHT_ROOT = "J_Bip_R_Middle1";
    public const string FINGER_MIDDLE_RIGHT_MID = "J_Bip_R_Middle2";
    public const string FINGER_MIDDLE_RIGHT_END = "J_Bip_R_Middle3";
    public const string FINGER_RING_RIGHT_ROOT = "J_Bip_R_Ring1";
    public const string FINGER_RING_RIGHT_MID = "J_Bip_R_Ring2";
    public const string FINGER_RING_RIGHT_END = "J_Bip_R_Ring3";
    public const string FINGER_THUMB_RIGHT_ROOT = "J_Bip_R_Thumb1";
    public const string FINGER_THUMB_RIGHT_MID = "J_Bip_R_Thumb2";
    public const string FINGER_THUMB_RIGHT_END = "J_Bip_R_Thumb3";
    public const string LEG_UPPER_LEFT = "J_Bip_L_UpperLeg";
    public const string LEG_LOWER_LEFT = "J_Bip_L_LowerLeg";
    public const string FOOT_LEFT = "J_Bip_L_Foot";
    public const string LEG_UPPER_RIGHT = "J_Bip_R_UpperLeg";
    public const string LEG_LOWER_RIGHT = "J_Bip_R_LowerLeg";
    public const string FOOT_RIGHT = "J_Bip_R_Foot";

    public const string ROOT = "Root";
    public const string FACE = "Face";
    public const string BODY = "Body";
    public const string HAIR = "Hair";
    public const string SECONDARY = "secondary";
}

public static class SECONDARY_BODY_PART_NAME {
    public const string CHEST = "Bust";
    public const string EAR_CAT = "CatEar";
    public const string SKIRT = "Skirt";
    public const string HAIR = "Hair";
}

enum PACKET_TYPE {
    NONE,
    HEAD_MOVEMENT,
    EXPRESSION,
    BODY_MOVEMENT
}

public static class utils {
    public static f32 square(f32 value) {
        return value * value;
    }
    
    public static f32 point_difference(Vector3 point_1, Vector3 point_2) {
        return (f32)Math.Sqrt((f64)(
            utils.square(point_1.x - point_2.x) +
            utils.square(point_1.y - point_2.y) +
            utils.square(point_1.z - point_2.z)
        ));
    }
    
    public static f32 point_difference_2d(f32 point_1_x, f32 point_1_y, f32 point_2_x, f32 point_2_y) {
        return (f32)Math.Sqrt((f64)(
            utils.square(point_1_x - point_2_x) +
            utils.square(point_1_y - point_2_y)
        ));
    }
    
    public static f32 pythagoras(f32 value_0, f32 value_1) {
        return (f32)Math.Sqrt((f64)(value_0 * value_0) + (f64)(value_1 * value_1));
    }
    
    public static f32 merge_values(f32 value_1, f32 value_2) {
        if (value_1 == 0.0f) {
            return value_2;
        } else if (value_2 == 0.0f) {
            return value_1;
        }

        if (value_1 < 0.0) {
            value_1 = -utils.square(value_1);
        } else {
            value_1 = utils.square(value_1);
        }

        if (value_2 < 0.0) {
            value_2 = -utils.square(value_2);
        } else {
            value_2 = utils.square(value_2);
        }

        f32 sum = value_1 + value_2;

        if (sum < 0.0) {
            return -(f32)Math.Sqrt((f64)(-sum));
        } else {
            return (f32)Math.Sqrt((f64)sum);
        }
    }
    
    public static u64 get_length_of(Array array) {
        return (u64)array.Length;
    }
    
    public static f32 string_to_float32(String value)
    {
        return f32.Parse(value);
    }
    
    public static f32[] string_to_float32(String[] string_array)
    {
        u64 array_length = utils.get_length_of(string_array);
        f32[] float32_array = new f32[array_length];
        
        for (u64 index = 0; index < array_length; index += 1) {
            float32_array[index] = string_to_float32(string_array[index]);
        }
        
        return float32_array;
    }
    
    public static ElementType[] slice_array<ElementType>(
        ElementType[] array,
        u64 start = 0,
        u64 end = u64.MaxValue,
        u64 step = 1
    ) {
        u64 array_length = utils.get_length_of(array);
        if (end > array_length) {
            end = array_length;
        }
        
        if (end < start) {
            end = start;
        }
        
        if (step == 0) {
            step = 1;
        }
        
        u64 checked_area = end - start;
        u64 new_array_length = checked_area / step + Convert.ToUInt64(checked_area % step != 0);
        
        ElementType[] new_array = new ElementType[new_array_length];
        
        u64 old_array_index = start;
        u64 new_array_index = 0;
        
        while (true) {
            if (old_array_index >= end) {
                break;
            }
            
            new_array[new_array_index] = array[old_array_index];
            
            new_array_index += 1;
            old_array_index += step;
        }
        
        return new_array;
    }
    
    public static f32 limit_target(f32 root_position, f32 new_position, f32 max_change) {
        f32 change = new_position - root_position;
        if (Math.Abs(change) > max_change) {
            if (new_position > root_position) {
                new_position = root_position + max_change;
            } else {
                new_position = root_position - max_change;
            }
        }
        
        return new_position;
    }
}


public class HeadMovementData {
    public f32 iris_left_x = 0.0f;
    public f32 iris_left_y = 0.0f;
    public f32 iris_right_x = 0.0f;
    public f32 iris_right_y = 0.0f;
    public f32 eye_openness_left = 0.0f;
    public f32 eye_openness_right = 0.0f;
    public f32 head_x = 0.0f;
    public f32 head_y = 0.0f;
    public f32 head_z = 0.0f;
    public f32 mouth_openness_x = 0.0f;
    public f32 mouth_openness_y = 0.0f;
    public f32 face_position_x = 0.0f;
    public f32 face_position_y = 0.0f;
    public f32 face_position_z = 0.0f;
    public f32 smile_ratio = 0.0f;
    public f32 eyebrow_liftedness = 0.0f;
    
    public HeadMovementData() {}
    
    public HeadMovementData(f32[] packet_data) {
        this.iris_left_x = packet_data[0];
        this.iris_left_y = packet_data[1];
        
        this.iris_right_x = packet_data[2];
        this.iris_right_y = packet_data[3];

        this.eye_openness_left = packet_data[4];
        this.eye_openness_right = packet_data[5];

        this.head_x = packet_data[6];
        this.head_y = packet_data[7];
        this.head_z = packet_data[8];

        this.mouth_openness_x = packet_data[9];
        this.mouth_openness_y = packet_data[10];

        this.face_position_y = packet_data[11];
        this.face_position_x = packet_data[12];
        this.face_position_z = packet_data[13];

        this.smile_ratio = packet_data[14];
        this.eyebrow_liftedness = packet_data[15];
    }
}


public class ExpressionData {
    public f32 happiness = 0.0f;
    public f32 sadness = 0.0f;
    public f32 surprise = 0.0f;
    public f32 fear = 0.0f;
    public f32 disgust = 0.0f;
    public f32 anger = 0.0f;
    
    public ExpressionData() {}
    
    public ExpressionData(f32[] packet_data) {
        this.happiness = packet_data[0];
        this.sadness = packet_data[1];
        this.surprise = packet_data[2];
        this.fear = packet_data[3];
        this.disgust = packet_data[4];
        this.anger = packet_data[5];
    }
}


public class BodyMovementData {
    public f32 arm_upper_left_x = 0.0f;
    public f32 arm_upper_left_z = 0.0f;
    public f32 arm_upper_right_x = 0.0f;
    public f32 arm_upper_right_z = 0.0f;
    public f32 back_y = 0.0f;
    public f32 hip_x = 0.0f;
    public f32 hip_z = 0.0f;
    public f32 shoulder_x = 0.0f;
    public f32 shoulder_z = 0.0f;
    
    public BodyMovementData() {}
    
    public BodyMovementData(f32[] packet_data) {
        this.arm_upper_left_x = packet_data[0];
        this.arm_upper_left_z = packet_data[1];
        this.arm_upper_right_x = packet_data[2];
        this.arm_upper_right_z = packet_data[3];
        this.back_y = packet_data[4];
        this.hip_x = packet_data[5];
        this.hip_z = packet_data[6];
        this.shoulder_x = packet_data[7];
        this.shoulder_z = packet_data[8];
    }
}


public class ModelControl : MonoBehaviour {
    [Header("Avatar")]
    [SerializeField]
    public Avatar avatar;

    [Header("Camera")]
    [SerializeField]
    [Range(-15.0f, +15.0f)]
    public f32 camera_angle_vertical = -15.0f;

    [Header("Mouth")]
    [SerializeField]
    [Range(50.0f, 80.0f)]
    public f32 default_mouth_openness_horizontal = +73.0f;

    [SerializeField]
    [Range(10.0f, 40.0f)]
    public f32 default_mouth_openness_vertical = +26.0f;

    [Header("Eyes")]
    [SerializeField]
    [Range(30.0f, 70.0f)]
    public f32 open_eye_at = 50.0f;
    [SerializeField]
    [Range(10.0f, 50.0f)]
    public f32 close_eye_at = 25.0f;

    [Header("Position")]
    [SerializeField]
    public bool auto_set_position_adjustment = true;
    private bool should_auto_set_position_adjustment = false;
    [Range(-10.0f, 10.0f)]
    public f32 position_adjustment_deepness = 0.0f;
    [Range(-10.0f, 10.0f)]
    public f32 position_adjustment_horizontal = 0.0f;
    [Range(-10.0f, 10.0f)]
    public f32 position_adjustment_vertical = 0.0f;
    [SerializeField]
    [Range(0.0f, 10.0f)]
    public f32 max_position_change = 0.01f;
    [SerializeField]
    public bool lock_position_deepness = true;
    [SerializeField]
    public bool lock_position_horizontal = true;
    [SerializeField]
    public bool lock_position_vertical = true;

    [Header("Smoothing")]
    [SerializeField]
    [Range(1, 60)]
    public i32 application_fps = 30;
    [Range(1, 60)]
    public i32 server_fps = 15;

    private i32 smooth_level = 1;
    private i32 head_movement_smooth_step = 0;
    private i32 expression_smooth_step = 0;
    private i32 body_movement_smooth_step = 0;

    private const f32 blinkage_speed = 12.0f;
    private i32 blinkage_steps = 8;
    
    [Header("Adjustment")]
    public bool readjust = false;
    
    [Header("Hair")]
    [SerializeField]
    [Range(0.0f, 4.0f)]
    public f32 hair_stiffness = 0.20f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    public f32 hair_gravity = 0.08f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    public f32 hair_drag = 1.0f;


    [Header("Chest")]
    [SerializeField]
    [Range(0.0f, 4.0f)]
    public f32 chest_stiffness = 0.2f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    public f32 chest_gravity = 0.05f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    public f32 chest_drag = 0.2f;
    
    [Header("Input")]
    [SerializeField]
    public bool lock_head_movements = false;
    [SerializeField]
    public bool lock_expressions = false;
    [SerializeField]
    public bool lock_body_movements = false;

    [Header("Head Movements")]
    [Range(-16.0f, +8.0f)]
    public f32 iris_left_x = 0.0f;
    [Range(-10.0f, +10.0f)]
    public f32 iris_left_y = 0.0f;
    [Range(-8.0f, +16.0f)]
    public f32 iris_right_x = 0.0f;
    [Range(-10.0f, +10.0f)]
    public f32 iris_right_y = 0.0f;

    [Range(+0.0f, +100.0f)]
    public f32 eye_openness_left = 0.0f;
    [Range(+0.0f, 100.0f)]
    public f32 eye_openness_right = 0.0f;

    [Range(-90.0f, +90.0f)]
    public f32 head_x = 0.0f;
    [Range(-60.0f, +30.0f)]
    public f32 head_y = 0.0f;
    [Range(-90.0f, +90.0f)]
    public f32 head_z = 0.0f;

    [Range(-0.0f, +100.0f)]
    public f32 mouth_openness_x = 0.0f;
    [Range(-0.0f, +100.0f)]
    public f32 mouth_openness_y = 0.0f;

    [Range(-5.0f, +5.0f)]
    public f32 face_position_x = 0.0f;
    [Range(-5.0f, +5.0f)]
    public f32 face_position_y = 0.0f;
    [Range(-10.0f, -1.0f)]
    public f32 face_position_z = 0.0f;

    [Range(0.0f, 100.0f)]
    public f32 smile_ratio = 0.0f;
    [Range(0.0f, 100.0f)]
    public f32 eyebrow_liftedness = 0.0f;

    [Header("Expressions")]
    [Range(0.0f, 100.0f)]
    public f32 happiness = 0.0f;
    [Range(0.0f, 100.0f)]
    public f32 sadness = 0.0f;
    [Range(0.0f, 100.0f)]
    public f32 surprise = 0.0f;
    [Range(0.0f, 100.0f)]
    public f32 fear = 0.0f;
    [Range(0.0f, 100.0f)]
    public f32 disgust = 0.0f;
    [Range(0.0f, 100.0f)]
    public f32 anger = 0.0f;
    
    [Header("Body")]
    [Range(-180.0f, +180f)]
    public f32 arm_upper_left_x = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 arm_upper_left_z = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 arm_upper_right_x = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 arm_upper_right_z = 0.0f;
    [Range(0.0f, +90f)]
    public f32 back_y = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 hip_x = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 hip_z = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 shoulder_x = 0.0f;
    [Range(-180.0f, +180f)]
    public f32 shoulder_z = 0.0f;
    
    // Movements
    
    private HeadMovementData head_movement_target = null;
    private ExpressionData expression_target = null;
    private BodyMovementData body_movement_target = null;
    
    private HeadMovementData head_movement_step = null;
    private ExpressionData expression_step = null;
    private BodyMovementData body_movement_step = null;

    // body parts
    private GameObject eye_right = null;
    private GameObject eye_left = null;

    private GameObject neck = null;
    private GameObject head = null;

    private GameObject shoulder_right = null;
    private GameObject shoulder_left = null;
    private GameObject arm_upper_right = null;
    private GameObject arm_upper_left = null;
    private GameObject arm_lower_right = null;
    private GameObject arm_lower_left = null;
    private f32 shoulder_length = 0.0f;
    private f32 root_to_head = 1.6f;
    private GameObject body_root = null;
    private GameObject hips = null;
    private GameObject spine = null;
    private GameObject chest = null;
    private GameObject chest_upper = null;

    private SkinnedMeshRenderer face_mesh = null;

    // Thread
    private Thread receiver_thread = null;
    private TcpListener tcp_listener = null;
    private TcpClient tcp_client = null;
    
    // Connection
    private i32 port = 5050;
    private string address = "127.0.0.1";
    
    // eye close
    private bool is_left_eye_closing = false;
    private bool is_right_eye_closing = false;

    private bool did_left_eye_close = false;
    private bool did_right_eye_close = false;

    private i32 left_eye_close_step = 0;
    private i32 right_eye_close_step = 0;


    /* This will be changed at startup, no worries */
    private f32 head_size = 1.0f;
    private Vector3 root_position = new Vector3(0.0f, 0.0f, 0.0f);

    // Expressions
    private f32 expression_eye_close_left = 0.0f;
    private f32 expression_eye_close_right = 0.0f;
    private f32 expression_eye_fun = 0.0f;
    private f32 expression_eye_surprised = 0.0f;
    private f32 expression_eye_joy_left = 0.0f;
    private f32 expression_eye_joy_right = 0.0f;
    private f32 expression_eye_angry = 0.0f;
    private f32 expression_eye_spread = 0.0f;
    private f32 expression_eye_sorrow = 0.0f;
    private f32 expression_eye_highlight_hide = 0.0f;

    private f32 expression_eyebrow_fun = 0.0f;
    private f32 expression_eyebrow_surprised = 0.0f;
    private f32 expression_eyebrow_angry = 0.0f;
    private f32 expression_eyebrow_joy = 0.0f;
    private f32 expression_eyebrow_sorrow = 0.0f;

    private f32 expression_mouth_a = 0.0f;
    private f32 expression_mouth_i = 0.0f;
    private f32 expression_mouth_u = 0.0f;
    private f32 expression_mouth_o = 0.0f;
    private f32 expression_mouth_fun = 0.0f;
    private f32 expression_mouth_surprised = 0.0f;
    private f32 expression_mouth_joy = 0.0f;
    private f32 expression_mouth_angry = 0.0f;
    private f32 expression_mouth_neutral = 0.0f;
    private f32 expression_mouth_up = 0.0f;
    private f32 expression_mouth_sorrow = 0.0f;

    private f32 expression_teeth_short_bot = 0.0f;
    private f32 expression_teeth_short_top = 0.0f;

    // Connection Methods
    
    void maybe_start_tpc_connection() {
        if (this.receiver_thread == null) {
            this.start_tpc_connection();
        }
    }
    
    void start_tpc_connection() {
        Thread receiver_thread = new Thread(new ThreadStart(this.receive_data));
        receiver_thread.IsBackground = true;
        receiver_thread.Start();
        this.receiver_thread = receiver_thread;
    }
    
    private void receive_data() {
        TcpListener tcp_listener = null;
        
        TcpClient tcp_client;
        Byte[] payload_buffer;
        u64 length;
        NetworkStream stream;
        
        try {          
            tcp_listener = new TcpListener(IPAddress.Parse(this.address), this.port);
            
            tcp_listener.Start();
            this.tcp_listener = tcp_listener;
            
            payload_buffer = new Byte[1024];
            
            while (true) {
                using (tcp_client = tcp_listener.AcceptTcpClient()) {
                    this.tcp_client = tcp_client;
                    
                    using (stream = tcp_client.GetStream()) {
                        while (true) {
                            length = (u64)stream.Read(payload_buffer, 0, (i32)utils.get_length_of(payload_buffer));
                            if (length == 0) {
                                break;
                            }
                            
                            var received_payload = new byte[length];
                            Array.Copy(payload_buffer, 0, received_payload, 0, (i32)length);
                            
                            string received_message = Encoding.ASCII.GetString(received_payload);
                            this.process_packet(received_message.Split(' '));
                        }
                    }
                }
            }
        } catch(Exception e) {
            print (e.ToString());
        
        } finally {
            if (tcp_listener != null) {
                tcp_listener.Stop();
            }
            
            this.tcp_listener = null;
            this.tcp_client = null;
            this.receiver_thread = null;
        }
    }

    public void process_packet(string[] values) {
        PACKET_TYPE packet_type = (PACKET_TYPE)i32.Parse(values[0]);

        f32[] packet_data = utils.string_to_float32(utils.slice_array<String>(values, start: 1));

        if (packet_type == PACKET_TYPE.HEAD_MOVEMENT) {
            this.set_head_movement(packet_data);
        } else if (packet_type == PACKET_TYPE.EXPRESSION) {
            this.set_expression(packet_data);
        } else if (packet_type == PACKET_TYPE.BODY_MOVEMENT) {
            this.set_body_movement(packet_data);
        }
    }

    void set_head_movement(f32[] packet_data) {
        this.head_movement_target = new HeadMovementData(packet_data);
        this.head_movement_smooth_step = 0;
    }

    void set_expression(f32[] packet_data) {
        this.expression_target = new ExpressionData(packet_data);
        this.expression_smooth_step = 0;
    }

    void set_body_movement(f32[] packet_data) {
        this.body_movement_target = new BodyMovementData(packet_data);
        this.body_movement_smooth_step = 0;
    }


    void do_head_movement_smooth_step(HeadMovementData head_movement_target) {
        if (head_movement_target == null) {
            return;
        }
        
        i32 smooth_level = this.smooth_level;
        i32 smooth_step = this.head_movement_smooth_step;
        this.head_movement_smooth_step = smooth_step + 1;

        if (smooth_level == 1) {
            if (smooth_step == 0) {
                if (! this.lock_head_movements) {
                    this.iris_left_x = head_movement_target.iris_left_x;
                    this.iris_left_y = head_movement_target.iris_left_y;
                    this.iris_right_x = head_movement_target.iris_right_x;
                    this.iris_right_y = head_movement_target.iris_right_y;
                    this.eye_openness_left = head_movement_target.eye_openness_left;
                    this.eye_openness_right = head_movement_target.eye_openness_right;
                    this.head_x = head_movement_target.head_x;
                    this.head_y = head_movement_target.head_y;
                    this.head_z = head_movement_target.head_z;
                    this.mouth_openness_x = head_movement_target.mouth_openness_x;
                    this.mouth_openness_y = head_movement_target.mouth_openness_y;
                    this.face_position_x = head_movement_target.face_position_x;
                    this.face_position_y = head_movement_target.face_position_y;
                    this.face_position_z = head_movement_target.face_position_z;
                    this.smile_ratio = head_movement_target.smile_ratio;
                    this.eyebrow_liftedness = head_movement_target.eyebrow_liftedness;
                }
            }
        } else {
            HeadMovementData head_movement_step;
            
            if (smooth_step == 0) {
                head_movement_step = new HeadMovementData();
                this.head_movement_step = head_movement_step;
                
                head_movement_step.iris_left_x = (head_movement_target.iris_left_x - this.iris_left_x) / smooth_level;
                head_movement_step.iris_left_y = (head_movement_target.iris_left_y - this.iris_left_y) / smooth_level;
                head_movement_step.iris_right_x = (head_movement_target.iris_right_x - this.iris_right_x) / smooth_level;
                head_movement_step.iris_right_y = (head_movement_target.iris_right_y - this.iris_right_y) / smooth_level;
                head_movement_step.eye_openness_left = (
                    (head_movement_target.eye_openness_left - this.eye_openness_left) / smooth_level
                );
                head_movement_step.eye_openness_right = (
                    (head_movement_target.eye_openness_right - this.eye_openness_right) / smooth_level
                );
                head_movement_step.head_x = (head_movement_target.head_x - this.head_x) / smooth_level;
                head_movement_step.head_y = (head_movement_target.head_y - this.head_y) / smooth_level;
                head_movement_step.head_z = (head_movement_target.head_z - this.head_z) / smooth_level;
                head_movement_step.mouth_openness_x = (head_movement_target.mouth_openness_x - this.mouth_openness_x) / smooth_level;
                head_movement_step.mouth_openness_y = (head_movement_target.mouth_openness_y - this.mouth_openness_y) / smooth_level;
                head_movement_step.face_position_x = (head_movement_target.face_position_x - this.face_position_x) / smooth_level;
                head_movement_step.face_position_y = (head_movement_target.face_position_y - this.face_position_y) / smooth_level;
                head_movement_step.face_position_z = (head_movement_target.face_position_z - this.face_position_z) / smooth_level;
                head_movement_step.smile_ratio = (head_movement_target.smile_ratio - this.smile_ratio) / smooth_level;
                head_movement_step.eyebrow_liftedness = (head_movement_target.eyebrow_liftedness - this.eyebrow_liftedness) / smooth_level;

            } else if (smooth_step >= smooth_level) {
                return;
            } else {
                head_movement_step = this.head_movement_step;
                if (head_movement_step == null) {
                    return;
                }
            }
            
            if (! this.lock_head_movements) {
                this.iris_left_x += head_movement_step.iris_left_x;
                this.iris_left_y += head_movement_step.iris_left_y;
                this.iris_right_x += head_movement_step.iris_right_x;
                this.iris_right_y += head_movement_step.iris_right_y;
                this.eye_openness_left += head_movement_step.eye_openness_left;
                this.eye_openness_right += head_movement_step.eye_openness_right;
                this.head_x += head_movement_step.head_x;
                this.head_y += head_movement_step.head_y;
                this.head_z += head_movement_step.head_z;
                this.mouth_openness_x += head_movement_step.mouth_openness_x;
                this.mouth_openness_y += head_movement_step.mouth_openness_y;
                this.face_position_x += head_movement_step.face_position_x;
                this.face_position_y += head_movement_step.face_position_y;
                this.face_position_z += head_movement_step.face_position_z;
                this.smile_ratio += head_movement_step.smile_ratio;
                this.eyebrow_liftedness += head_movement_step.eyebrow_liftedness;
            }
        }
        
        if (smooth_step == 0) {
            // We may have just receive this the first time. Check whether we should auto adjust.
            if (this.should_auto_set_position_adjustment) {
                this.should_auto_set_position_adjustment = false;
                this.position_adjustment_deepness = -head_movement_target.face_position_z * head_size;
                this.position_adjustment_horizontal = -head_movement_target.face_position_x * head_size;
                this.position_adjustment_vertical = -head_movement_target.face_position_y * head_size;
            }
        }
    }

    void do_expression_smooth_step(ExpressionData expression_target) {
        if (expression_target == null) {
            return;
        }
        
        i32 smooth_level = this.smooth_level;
        i32 smooth_step = this.expression_smooth_step;
        this.expression_smooth_step = smooth_step + 1;

        if (smooth_level == 1) {
            if (smooth_step == 0) {
                if (! this.lock_expressions) {
                    this.happiness = expression_target.happiness;
                    this.sadness = expression_target.sadness;
                    this.surprise = expression_target.surprise;
                    this.fear = expression_target.fear;
                    this.disgust = expression_target.disgust;
                    this.anger = expression_target.anger;
                }
            }
        } else {
            ExpressionData expression_step;
            
            if (smooth_step == 0) {
                expression_step = new ExpressionData();
                this.expression_step = expression_step;
            
                expression_step.happiness = (expression_target.happiness - this.happiness) / smooth_level;
                expression_step.sadness = (expression_target.sadness - this.sadness) / smooth_level;
                expression_step.surprise = (expression_target.surprise - this.surprise) / smooth_level;
                expression_step.fear = (expression_target.fear - this.fear) / smooth_level;
                expression_step.disgust = (expression_target.disgust - this.disgust) / smooth_level;
                expression_step.anger = (expression_target.anger - this.anger) / smooth_level;

            } else if (smooth_step >= smooth_level) {
                return;
            } else {
                expression_step = this.expression_step;
                if (expression_step == null) {
                    return;
                }
            }
            if (! this.lock_expressions) {
                this.happiness += expression_step.happiness;
                this.sadness += expression_step.sadness;
                this.surprise += expression_step.surprise;
                this.fear += expression_step.fear;
                this.disgust += expression_step.disgust;
                this.anger += expression_step.anger;
            }
        }
    }

    void do_body_movement_smooth_step(BodyMovementData body_movement_target) {
        if (body_movement_target == null) {
            return;
        }
        
        i32 smooth_level = this.smooth_level;
        i32 smooth_step = this.body_movement_smooth_step;
        this.body_movement_smooth_step = smooth_step + 1;

        if (smooth_level == 1) {
            if (smooth_step == 0) {
                if (! this.lock_body_movements) {
                    this.arm_upper_left_x = body_movement_target.arm_upper_left_x;
                    this.arm_upper_left_z = body_movement_target.arm_upper_left_z;
                    this.arm_upper_right_x = body_movement_target.arm_upper_right_x;
                    this.arm_upper_right_z = body_movement_target.arm_upper_right_z;
                    this.back_y = body_movement_target.back_y;
                    this.hip_x = body_movement_target.hip_x;
                    this.hip_z = body_movement_target.hip_z;
                    this.shoulder_x = body_movement_target.shoulder_x;
                    this.shoulder_z = body_movement_target.shoulder_z;
                }
            }
        } else {
            BodyMovementData body_movement_step;
            
            if (smooth_step == 0) {
                body_movement_step = new BodyMovementData();
                this.body_movement_step = body_movement_step;
                
                body_movement_step.arm_upper_left_x = (
                    (body_movement_target.arm_upper_left_x - this.arm_upper_left_x) / smooth_level
                );
                body_movement_step.arm_upper_left_z = (
                    (body_movement_target.arm_upper_left_z - this.arm_upper_left_z) / smooth_level
                );
                body_movement_step.arm_upper_right_x = (
                    (body_movement_target.arm_upper_right_x - this.arm_upper_right_x) / smooth_level
                );
                body_movement_step.arm_upper_right_z = (
                    (body_movement_target.arm_upper_right_z - this.arm_upper_right_z) / smooth_level
                );
                body_movement_step.back_y = (body_movement_target.back_y - this.back_y) / smooth_level;
                body_movement_step.hip_x = (body_movement_target.hip_x - this.hip_x) / smooth_level;
                body_movement_step.hip_z = (body_movement_target.hip_z - this.hip_z) / smooth_level;
                body_movement_step.shoulder_x = (body_movement_target.shoulder_x - this.shoulder_x) / smooth_level;
                body_movement_step.shoulder_z = (body_movement_target.shoulder_z - this.shoulder_z) / smooth_level;
                
            } else if (smooth_step >= smooth_level) {
                return;
            } else {
                body_movement_step = this.body_movement_step;
                if (body_movement_step == null) {
                    return;
                }
            }
            if (! this.lock_body_movements) {
                this.arm_upper_left_x += body_movement_step.arm_upper_left_x;
                this.arm_upper_left_z += body_movement_step.arm_upper_left_z;
                this.arm_upper_right_x += body_movement_step.arm_upper_right_x;
                this.arm_upper_right_z += body_movement_step.arm_upper_right_z;
                this.back_y += body_movement_step.back_y;
                this.hip_x += body_movement_step.hip_x;
                this.hip_z += body_movement_step.hip_z;
                this.shoulder_x += body_movement_step.shoulder_x;
                this.shoulder_z += body_movement_step.shoulder_z;
            }
        }
    }

    // Utility methods

    GameObject find_child_recursive(Transform parent_transform, string name) {
        GameObject child = null;

        foreach (Transform child_transform in parent_transform) {
            if (child_transform.name == name) {
                child = child_transform.gameObject;
                break;
            }

            child = this.find_child_recursive(child_transform, name);
            if (child != null) {
                break;
            }
        }

        return child;
    }


    GameObject get_child_with_name(string name) {
        return this.find_child_recursive(this.gameObject.transform, name);
    }

    VRMSpringBone get_secondary_body_bone(string name) {
        VRMSpringBone found_bone = null;

        GameObject secondary_body_parts = this.get_child_with_name(BODY_PART_NAME.SECONDARY);
        if (secondary_body_parts != null) {
            VRMSpringBone[] spring_bones = secondary_body_parts.GetComponents<VRMSpringBone>();
            if (spring_bones != null) {
                foreach (VRMSpringBone spring_bone in spring_bones) {
                    if (spring_bone.m_comment == name) {
                        found_bone = spring_bone;
                        break;
                    }
                }
            }
        }
        return found_bone;
    }

    IEnumerable<VRMSpringBone> iter_secondary_body_bones(string name) {
        GameObject secondary_body_parts = this.get_child_with_name(BODY_PART_NAME.SECONDARY);
        if (secondary_body_parts != null) {
            VRMSpringBone[] spring_bones = secondary_body_parts.GetComponents<VRMSpringBone>();
            if (spring_bones != null) {
                foreach (VRMSpringBone spring_bone in spring_bones) {
                    if (spring_bone.m_comment == name) {
                        yield return spring_bone;
                    }
                }
            }
        }
    }

    // Initialization

    void try_set_body_parts() {
        // eyes & head & neck
        this.eye_right = this.get_child_with_name(BODY_PART_NAME.EYE_RIGHT);
        this.eye_left = this.get_child_with_name(BODY_PART_NAME.EYE_LEFT);

        this.head = this.get_child_with_name(BODY_PART_NAME.HEAD);
        this.neck = this.get_child_with_name(BODY_PART_NAME.NECK);

        // Face

        GameObject face = this.get_child_with_name(BODY_PART_NAME.FACE);
        SkinnedMeshRenderer face_mesh;
        if (face == null) {
            face_mesh = null;
        } else {
            face_mesh = face.GetComponent<SkinnedMeshRenderer>();
        }

        this.face_mesh = face_mesh;

        // shoulder
        this.shoulder_right = this.get_child_with_name(BODY_PART_NAME.SHOULDER_RIGHT);
        this.shoulder_left = this.get_child_with_name(BODY_PART_NAME.SHOULDER_LEFT);

        // arm
        this.arm_upper_right = this.get_child_with_name(BODY_PART_NAME.ARM_UPPER_RIGHT);
        this.arm_upper_left = this.get_child_with_name(BODY_PART_NAME.ARM_UPPER_LEFT);
        this.arm_lower_right = this.get_child_with_name(BODY_PART_NAME.ARM_LOWER_RIGHT);
        this.arm_lower_left = this.get_child_with_name(BODY_PART_NAME.ARM_LOWER_LEFT);

        // root

        this.body_root = this.get_child_with_name(BODY_PART_NAME.ROOT);
        
        // body
        this.hips = this.get_child_with_name(BODY_PART_NAME.HIPS);
        this.spine = this.get_child_with_name(BODY_PART_NAME.SPINE);
        this.chest = this.get_child_with_name(BODY_PART_NAME.CHEST);
        this.chest_upper = this.get_child_with_name(BODY_PART_NAME.CHEST_UPPER);
    }


    void try_adjust_colliders_of(GameObject game_object, f32 by) {
        VRMSpringBoneColliderGroup collider_groups;
        if (game_object == null) {
            collider_groups = null;
        } else {
            collider_groups = game_object.GetComponent<VRMSpringBoneColliderGroup>();
        }

        if (collider_groups != null) {
            VRMSpringBoneColliderGroup.SphereCollider[] Colliders = collider_groups.Colliders;

            foreach (VRMSpringBoneColliderGroup.SphereCollider collider in Colliders) {
                collider.Radius = collider.Radius * by;
            }
        }
    }

    void try_adjust_colliders() {
        // Arms
        this.try_adjust_colliders_of(this.arm_upper_right, 1.5f);
        this.try_adjust_colliders_of(this.arm_upper_left, 1.5f);
        this.try_adjust_colliders_of(this.arm_lower_right, 1.8f);
        this.try_adjust_colliders_of(this.arm_lower_left, 1.8f);

        //head
        this.try_adjust_colliders_of(this.head, 1.05f);

        // Chest / upper

        GameObject chest_upper = this.chest_upper;

        VRMSpringBoneColliderGroup chest_upper_collider_group;
        if (chest_upper == null) {
            chest_upper_collider_group = null;
        } else {
            chest_upper_collider_group = chest_upper.GetComponent<VRMSpringBoneColliderGroup>();
        }

        if (chest_upper_collider_group != null) {
            VRMSpringBoneColliderGroup.SphereCollider[] colliders = chest_upper_collider_group.Colliders;
            if (colliders.Length == 3) {
                VRMSpringBoneColliderGroup.SphereCollider left_collider = colliders[1];
                left_collider.Radius = left_collider.Radius * 1.5f;

                VRMSpringBoneColliderGroup.SphereCollider right_collider = colliders[2];
                right_collider.Radius = right_collider.Radius * 1.5f;

                // Chest / upper / chest

                GameObject chest_left_root = this.get_child_with_name(BODY_PART_NAME.CHEST_LEFT_ROOT);
                GameObject chest_right_root = this.get_child_with_name(BODY_PART_NAME.CHEST_RIGHT_ROOT);
                GameObject chest_left_mid = this.get_child_with_name(BODY_PART_NAME.CHEST_LEFT_MID);
                GameObject chest_right_mid = this.get_child_with_name(BODY_PART_NAME.CHEST_RIGHT_MID);

                if (
                    (chest_left_root != null) &&
                    (chest_right_root != null) &&
                    (chest_left_mid != null) &&
                    (chest_right_mid != null)
                ) {
                    Vector3 chest_upper_position = chest_upper.transform.position;

                    /* Extract the upper torso value from the chest positions, because they are absolute at this case */
                    Vector3 chest_left_root_position = chest_left_root.transform.position - chest_upper_position;
                    Vector3 chest_right_root_position = chest_right_root.transform.position - chest_upper_position;
                    Vector3 chest_left_mid_position = chest_left_mid.transform.position - chest_upper_position;
                    Vector3 chest_right_mid_position = chest_right_mid.transform.position - chest_upper_position;

                    f32 collision_box_size = (
                        utils.point_difference(chest_left_root_position, chest_left_mid_position) +
                        utils.point_difference(chest_right_root_position, chest_right_mid_position)
                    );

                    VRMSpringBoneColliderGroup.SphereCollider[] new_colliders = (
                        new VRMSpringBoneColliderGroup.SphereCollider[7] {
                            colliders[0],
                            left_collider,
                            right_collider,
                            new VRMSpringBoneColliderGroup.SphereCollider {
                                Offset = chest_left_root_position,
                                Radius = collision_box_size * 0.6f
                            },
                            new VRMSpringBoneColliderGroup.SphereCollider {
                                Offset = chest_left_root_position + chest_left_mid_position,
                                Radius = collision_box_size * 0.3f
                            },
                            new VRMSpringBoneColliderGroup.SphereCollider {
                                Offset = chest_right_root_position,
                                Radius = collision_box_size * 0.6f
                            },
                            new VRMSpringBoneColliderGroup.SphereCollider {
                                Offset = chest_right_root_position + chest_right_mid_position,
                                Radius = collision_box_size * 0.3f
                            }
                        }
                    );
                    chest_upper_collider_group.Colliders = new_colliders;
                }
            }
        }

        // Neck
        GameObject neck = this.neck;
        GameObject head = this.head;

        if ((neck != null) && (head != null)) {
            VRMSpringBoneColliderGroup neck_collider_group = neck.GetComponent<VRMSpringBoneColliderGroup>();
            if (neck_collider_group != null) {
                VRMSpringBoneColliderGroup.SphereCollider[] colliders = neck_collider_group.Colliders;
                if (colliders.Length == 1) {
                    VRMSpringBoneColliderGroup.SphereCollider collider = colliders[0];
                    f32 radius = collider.Radius * 1.1f;
                    collider.Radius = radius;
                    Vector3 position_difference = head.transform.position - neck.transform.position;

                    VRMSpringBoneColliderGroup.SphereCollider[] new_colliders = (
                        new VRMSpringBoneColliderGroup.SphereCollider[3] {
                            collider,
                            new VRMSpringBoneColliderGroup.SphereCollider {
                                Offset = position_difference * 0.33f,
                                Radius = radius
                            },
                            new VRMSpringBoneColliderGroup.SphereCollider {
                                Offset = position_difference * 0.67f,
                                Radius = radius
                            }
                        }
                    );
                    neck_collider_group.Colliders = new_colliders;
                }
            }
        }

    }

    void try_adjust_chest_gravity() {
        VRMSpringBone chest_bone = this.get_secondary_body_bone(SECONDARY_BODY_PART_NAME.CHEST);
        if (chest_bone != null) {
            chest_bone.m_stiffnessForce = this.chest_stiffness;
            chest_bone.m_gravityPower = this.chest_gravity;
            chest_bone.m_dragForce = this.chest_drag;
        }
    }


    void try_adjust_ear_cat_gravity() {
        VRMSpringBone ear_cat  = this.get_secondary_body_bone(SECONDARY_BODY_PART_NAME.EAR_CAT);
        if (ear_cat != null) {
            ear_cat.m_stiffnessForce = 4.0f;
            ear_cat.m_gravityPower = 0.0f;
            ear_cat.m_dragForce = 1.0f;
        }
    }

    void try_adjust_hair_gravity() {
        foreach (VRMSpringBone hair in this.iter_secondary_body_bones(SECONDARY_BODY_PART_NAME.HAIR)) {
            hair.m_stiffnessForce = this.hair_stiffness;
            hair.m_gravityPower = this.hair_gravity;
            hair.m_dragForce = this.hair_drag;
        }
    }

    void set_shoulder_length() {
        GameObject shoulder_right = this.shoulder_right;
        GameObject shoulder_left = this.shoulder_left;
        GameObject arm_upper_right = this.arm_upper_right;
        GameObject arm_upper_left = this.arm_upper_left;

        if (
            (arm_upper_right != null) &&
            (arm_upper_left != null) &&
            (shoulder_right != null) &&
            (shoulder_left != null)
        ) {
            this.shoulder_length = (
                utils.point_difference(arm_upper_right.transform.position, shoulder_right.transform.position) +
                utils.point_difference(arm_upper_right.transform.position, shoulder_left.transform.position)
            ) * 0.5f;
        }
    }
    
    void set_root_to_head() {
        GameObject body_root = this.body_root;
        GameObject head = this.head;
        
        if ((body_root != null) && (head != null)) {
            this.root_to_head = utils.point_difference(body_root.transform.position, head.transform.position);
        }
    }

    bool maybe_readjust() {
        bool readjust = this.readjust;
        if (readjust) {
            this.invoke_adjustments();
        }
        return readjust;
    }
    
    void invoke_adjustments() {
        this.readjust = false;
        this.try_adjust_colliders();
        this.try_adjust_chest_gravity();
        this.try_adjust_ear_cat_gravity();
        this.try_adjust_hair_gravity();
    }

    void set_head_size() {
        GameObject head = this.head;
        if (head != null) {
            VRMSpringBoneColliderGroup head_collider_group = head.GetComponent<VRMSpringBoneColliderGroup>();
            if (head_collider_group != null) {
                VRMSpringBoneColliderGroup.SphereCollider[] colliders = head_collider_group.Colliders;
                if (colliders.Length == 1) {
                    VRMSpringBoneColliderGroup.SphereCollider collider = colliders[0];
                    this.head_size = collider.Radius;
                }
            }
        }
    }

    void set_root_position() {
        GameObject body_root = this.body_root;
        if (body_root != null) {
            this.root_position =  body_root.transform.position;
        }
    }

    void set_fps() {
        i32 application_fps = this.application_fps;
        Application.targetFrameRate = application_fps;

        i32 smooth_level = Convert.ToInt32(
            Math.Ceiling(
                Convert.ToDouble((application_fps)) /
                Convert.ToDouble((this.server_fps))
            )
        );

        if (smooth_level <= 0) {
            smooth_level = 1;
        }
        this.smooth_level = smooth_level;

        i32 blinkage_steps = Convert.ToInt32(
            Math.Ceiling(
                (f64)(1.0f / ModelControl.blinkage_speed) *
                Convert.ToDouble(application_fps)
            )
        );

        if (blinkage_steps <= 0) {
            blinkage_steps = 1;
        }
        this.blinkage_steps = blinkage_steps;
    }

    // Start is called before the first frame update

    void Start() {
        if (this.auto_set_position_adjustment) {
            this.should_auto_set_position_adjustment = true;
        }
        
        this.try_set_body_parts();

        this.set_head_size();
        this.set_root_position();

        this.invoke_adjustments();

        this.maybe_start_tpc_connection();

        this.set_fps();

        this.set_shoulder_length();
        this.set_root_to_head();
        
        this.head_y = this.camera_angle_vertical;
        this.shoulder_z = this.camera_angle_vertical;
        // Updates

        this.update_arms();
    }

    // Update
    f32 within_range(f32 current_value, f32 target_value, f32 value_range) {
        if (target_value > current_value) {
            current_value += value_range;

            if (target_value > current_value) {
                target_value = current_value;
            }

        } else if (target_value < current_value) {
            current_value -= value_range;

            if (target_value < current_value) {
                target_value = current_value;
            }
        }
        return target_value;
    }

    void update_irises() {
        GameObject head = this.head;
        GameObject eye_right = this.eye_right;
        GameObject eye_left = this.eye_left;

        if ((head != null) && (eye_right != null) && (eye_left != null)) {
            Quaternion base_rotation = head.transform.rotation;

            eye_right.transform.rotation = base_rotation * Quaternion.Euler(
                this.iris_right_y, this.iris_right_x, 0.0f
            );
            eye_left.transform.rotation = base_rotation * Quaternion.Euler(
                this.iris_left_y, this.iris_left_x, 0.0f
            );
        }
    }

    void update_head() {
        /* Rotate both head and neck by half of the total head rotation. This makes it more natural. */
        GameObject body_root = this.body_root;
        GameObject neck = this.neck;
        GameObject head = this.head;

        if ((body_root != null) && (neck != null) && (head != null)) {
            Quaternion root_rotation = body_root.transform.rotation;
            
            neck.transform.rotation = root_rotation * Quaternion.Euler(
                (this.head_y - this.camera_angle_vertical) * 0.5f,
                this.head_x * 0.5f,
                this.head_z * 0.5f
            );
            
            head.transform.rotation = root_rotation * Quaternion.Euler(
                this.head_y - this.camera_angle_vertical,
                this.head_x,
                this.head_z
            );
        }
    }


    void update_body() {
        /* Rotate spine and chest too. This makes it more natural. */
        GameObject body_root = this.body_root;
        GameObject hips = this.hips;
        GameObject spine = this.spine;
        GameObject chest = this.chest;
        GameObject chest_upper = this.chest_upper;

        if ((body_root != null) && (hips != null) && (spine != null) && (chest != null) && (chest_upper != null)) {
            Quaternion root_rotation = body_root.transform.rotation;
            
            f32 back_y = this.back_y;
            f32 hip_x = this.hip_x;
            f32 hip_z = this.hip_z;
            f32 shoulder_x = this.shoulder_x;
            f32 shoulder_z = this.shoulder_z;
            
            hips.transform.rotation = root_rotation * Quaternion.Euler(
                back_y,
                hip_x,
                hip_z
            );
            
            spine.transform.rotation = root_rotation * Quaternion.Euler(
                back_y,
                hip_x * 0.6f + shoulder_x * 0.4f,
                hip_z * 0.6f + shoulder_z * 0.4f
            );

            chest.transform.rotation = root_rotation * Quaternion.Euler(
                back_y,
                hip_x * 0.2f + shoulder_x * 0.8f,
                hip_z * 0.2f + shoulder_z * 0.8f
            );
            
            chest_upper.transform.rotation = root_rotation * Quaternion.Euler(
                back_y,
                shoulder_x,
                shoulder_z
            );
        }
    }


    void update_arms() {
        GameObject body_root = this.body_root;
        GameObject shoulder_right = this.shoulder_right;
        GameObject shoulder_left = this.shoulder_left;
        GameObject arm_upper_right = this.arm_upper_right;
        GameObject arm_upper_left = this.arm_upper_left;
        GameObject arm_lower_right = this.arm_lower_right;
        GameObject arm_lower_left = this.arm_lower_left;

        if (
            (body_root != null) &&
            (shoulder_right != null) &&
            (shoulder_left != null) &&
            (arm_upper_right != null) &&
            (arm_upper_left != null) &&
            (arm_lower_right != null) &&
            (arm_lower_left != null)
        ) {
            Quaternion root_rotation = body_root.transform.rotation;
            
            arm_upper_right.transform.rotation = body_root.transform.rotation *  Quaternion.Euler(
                0.0f, -this.arm_upper_right_x, -this.arm_upper_right_z
            );
            arm_upper_left.transform.rotation = body_root.transform.rotation *  Quaternion.Euler(
                0.0f, this.arm_upper_left_x, this.arm_upper_left_z
            );
            
            // Rotate lower arm if rotation high?
            /*
            if (right_arm_angle > 70.0f) {
                arm_lower_right.transform.rotation = body_root.transform.rotation * (
                    arm_upper_right_transform.rotation * Quaternion.Euler(0.0f, 0.0f, (right_arm_angle + 70.0f) * 0.5f)
                );
            } else {
                arm_lower_right.transform.rotation = arm_upper_right_transform.rotation;
            }

            if (left_arm_angle > 70.0f) {
                arm_lower_left.transform.rotation = body_root.transform.rotation * (
                    arm_upper_left_transform.rotation * Quaternion.Euler(0.0f, 0.0f, (left_arm_angle + 70.0f) * -0.5f)
                );
            } else {
                arm_lower_left.transform.rotation = arm_upper_left_transform.rotation;
            }
            */
            
            // Increase shoulder width if rotation < / > 0 up to 180° by 20%
            arm_upper_right.transform.position = (
                shoulder_right.transform.position +
                shoulder_right.transform.rotation * new Vector3(
                    this.shoulder_length * (
                        1.0f + Math.Min(Math.Max(this.arm_upper_right_z / 180.0f, 0.0f), 0.35f)
                    ),
                    0.0f,
                    0.0f
                )
            );

            arm_upper_left.transform.position = (
                shoulder_left.transform.position -
                shoulder_left.transform.rotation * new Vector3(
                    this.shoulder_length * (
                        1.0f + Math.Min(Math.Max(this.arm_upper_left_z / 180.0f, 0.0f), 0.35f)
                    ),
                    0.0f,
                    0.0f
                )
            );
        }
    }

    void update_position() {
        GameObject body_root = this.body_root;
        GameObject head = this.head;

        if ((body_root != null) && (head != null)) {
            f32 head_size = this.head_size;
            
            Vector3 head_difference = -(
                head.transform.position - body_root.transform.position - 
                (body_root.transform.rotation * new Vector3(0.0f, this.root_to_head, 0.0f))
            );
            
            Vector3 new_position = body_root.transform.rotation * new Vector3(
                this.face_position_y * head_size + this.position_adjustment_vertical + head_difference.x,
                this.face_position_x * head_size + this.position_adjustment_horizontal + head_difference.y,
                this.face_position_z * head_size + this.position_adjustment_deepness + head_difference.z
            );
            Vector3 current_position = body_root.transform.position;
            
            f32 new_position_x = current_position.x;
            if (! this.lock_position_horizontal) {
                new_position_x = utils.limit_target(new_position_x, new_position.x, this.max_position_change);
            }
            
            f32 new_position_y = current_position.y;
            if (! this.lock_position_horizontal) {
                new_position_y = utils.limit_target(new_position_y, new_position.y, this.max_position_change);
            }
            
            f32 new_position_z = current_position.z;
            if (! this.lock_position_deepness) {
                new_position_z = utils.limit_target(new_position_z, new_position.z, this.max_position_change);
            }
            
            body_root.transform.position = new Vector3(new_position_x, new_position_y, new_position_z);
        }
    }


    void set_eye_expressions(HeadMovementData head_movement_target) {
        f32 eye_openness_left;
        f32 eye_openness_right;
        
        if (head_movement_target == null) {
            eye_openness_left = 0f;
            eye_openness_right = 0f;
        } else {
            eye_openness_left = head_movement_target.eye_openness_left;
            eye_openness_right = head_movement_target.eye_openness_left;
            
        }
        
        bool is_left_eye_closing = this.is_left_eye_closing;
        bool is_right_eye_closing = this.is_right_eye_closing;
        i32 left_eye_close_step = this.left_eye_close_step;
        i32 right_eye_close_step = this.right_eye_close_step;
        bool did_left_eye_close = this.did_left_eye_close;
        bool did_right_eye_close = this.did_right_eye_close;
        f32 close_eye_at = this.close_eye_at;
        f32 open_eye_at = this.open_eye_at;

        bool should_close_left;
        bool should_close_right;

        // Detect whether we should close the eyes
        if (
            ((! did_left_eye_close) && (left_eye_close_step != 0)) ||
            (eye_openness_left <= close_eye_at) ||
            (
                (eye_openness_left <= open_eye_at) &&
                (
                    (eye_openness_right <= close_eye_at) ||
                    (is_left_eye_closing) ||
                    (is_right_eye_closing)
                )
            )
        ) {
            should_close_left = true;
        } else {
            should_close_left = false;
        }

        if (
            ((! did_right_eye_close) && (right_eye_close_step != 0)) ||
            (eye_openness_right <= close_eye_at) ||
            (
                (eye_openness_right <= open_eye_at) &&
                (
                    (eye_openness_left <= close_eye_at) ||
                    (is_left_eye_closing) ||
                    (is_right_eye_closing)
                )
            )
        ) {
            should_close_right = true;
        } else {
            should_close_right = false;
        }

        // Move the eyes

        if (should_close_left) {
            if (left_eye_close_step < this.blinkage_steps) {
                left_eye_close_step += 1;
            } else {
                this.did_left_eye_close = true;
            }

        } else {
            if (left_eye_close_step > 0) {
                left_eye_close_step -= 1;
            } else {
                this.did_left_eye_close = false;
            }
        }

        if (should_close_right) {
            if (right_eye_close_step < this.blinkage_steps) {
                right_eye_close_step += 1;
            } else {
                this.did_right_eye_close = true;
            }

        } else {
            if (right_eye_close_step > 0) {
                right_eye_close_step -= 1;
            } else {
                this.did_right_eye_close = false;
            }
        }

        this.expression_eye_close_left = left_eye_close_step * (70.0f / (f32)this.blinkage_steps);
        this.expression_eye_close_right = right_eye_close_step * (70.0f / (f32)this.blinkage_steps);

        // Store state
        this.left_eye_close_step = left_eye_close_step;
        this.right_eye_close_step = right_eye_close_step;

        this.is_left_eye_closing = should_close_left;
        this.is_right_eye_closing = should_close_right;
    }


    void set_mouth_expressions() {
        f32 mouth_x = this.mouth_openness_x - this.default_mouth_openness_horizontal;
        f32 mouth_y = this.mouth_openness_y - this.default_mouth_openness_vertical;
        f32 smile_ratio = this.smile_ratio;

        f32 expression_mouth_a = 0.0f;
        f32 expression_mouth_i = 0.0f;
        f32 expression_mouth_u = 0.0f;
        f32 expression_mouth_o = 0.0f;

        if (mouth_y > 6.0f) {
            expression_mouth_a = (mouth_y - 6.0f) * 1.5f;
            expression_mouth_o = expression_mouth_a / 3.0f;

            if (expression_mouth_a > 100.0f) {
                expression_mouth_o += expression_mouth_a - 100.0f;
                expression_mouth_a = 100.0f;
            }

            if (expression_mouth_o > 100.0f) {
                expression_mouth_o = 100.0f;
            }

        }

        if (mouth_x > 4.0f) {
            expression_mouth_i = (mouth_x - 4.0f) * 4.0f;

            if (expression_mouth_i > 100.0f) {
                expression_mouth_i = 100.0f;
            }
        }

        if ((mouth_x < -4.0f) && (mouth_y > 6.0f)){
            expression_mouth_u = (-mouth_x - 4.0f) * 4.0f;
            if (expression_mouth_u > 100.0f) {
                expression_mouth_u = 100.0f;
            }

            /* When u value is too high the face might overflow at places, so we will reduce a and o values */
            if (expression_mouth_u > 50.0f) {
                f32 openness_reduction = (expression_mouth_u - 50.0f) * 0.5f;

                expression_mouth_a -= openness_reduction;
                if (expression_mouth_a < 0.0f) {
                    expression_mouth_a = 0.0f;
                }

                expression_mouth_o -= openness_reduction;
                if (expression_mouth_o < 0.0f) {
                    expression_mouth_o = 0.0f;
                }
            }
        }

        // Reduce I by smile
        expression_mouth_i = expression_mouth_i - smile_ratio;
        if (expression_mouth_i < 0.0f) {
            expression_mouth_i = 0.0f;
        }

        this.expression_mouth_a = expression_mouth_a;
        this.expression_mouth_i = expression_mouth_i;
        this.expression_mouth_u = expression_mouth_u;
        this.expression_mouth_o = expression_mouth_o;
        this.expression_mouth_fun = smile_ratio;
        this.expression_teeth_short_bot = -expression_mouth_o;
    }


    void set_eyebrow_expressions() {
        this.expression_eyebrow_surprised = this.eyebrow_liftedness;
    }

    void update_happiness() {
        f32 happiness = this.happiness;

        // Cap on eye fun with blinkage
        f32 expression_eye_close_left = this.expression_eye_close_left;
        f32 expression_eye_close_right = this.expression_eye_close_right;

        f32 expression_eye_fun;

        if (expression_eye_close_left > expression_eye_close_right) {
            expression_eye_fun = expression_eye_close_left;
        } else {
            expression_eye_fun = expression_eye_close_right;
        }

        expression_eye_fun = Math.Max(happiness * 0.7f, expression_eye_fun);

        if (happiness > 0.0f) {
            f32 eye_joy_scale;
            if (happiness >= 80.0f) {
                eye_joy_scale = 1.0f;
            } else {
                eye_joy_scale = happiness * 0.0125f;
            }

            this.expression_eye_close_left = expression_eye_close_left * (1.0f - eye_joy_scale);
            this.expression_eye_close_right = expression_eye_close_right * (1.0f - eye_joy_scale);

            this.expression_eye_joy_right = utils.merge_values(
                this.expression_eye_joy_right,
                eye_joy_scale * expression_eye_close_left * (50f / 70f)
            );
            this.expression_eye_joy_left = utils.merge_values(
                expression_eye_joy_left,
                eye_joy_scale * expression_eye_close_right * (50f / 70f)
            );
        }

        this.expression_eye_fun = utils.merge_values(this.expression_eye_fun, expression_eye_fun);

        // Eyebrows
        this.expression_eyebrow_fun = utils.merge_values(this.expression_eyebrow_fun, happiness);

        // Mouth
        f32 expression_mouth_a = this.expression_mouth_a;
        f32 expression_mouth_o = this.expression_mouth_o;
        f32 expression_mouth_i = this.expression_mouth_i;

        f32 mouth_openness = (expression_mouth_a + expression_mouth_o) / 2.0f;
        f32 joy = Math.Min(mouth_openness, happiness * 3.0f);

        expression_mouth_o -= (joy * 2.0f);
        if (expression_mouth_o < 0.0f) {
            expression_mouth_a += expression_mouth_o;
            if (expression_mouth_a < 0.0f) {
                expression_mouth_a = 0.0f;
            }
            expression_mouth_o = 0.0f;
        }

        expression_mouth_i = Math.Max(expression_mouth_i - joy, 0.0f);

        this.expression_mouth_a = expression_mouth_a;
        this.expression_mouth_o = expression_mouth_o;
        this.expression_mouth_i = expression_mouth_i;
        this.expression_mouth_joy = utils.merge_values(this.expression_mouth_joy, joy);
    }

    void update_surprised() {
        f32 surprise = this.surprise;
        // Update eyebrow even if we don not do `o` face
        this.expression_eyebrow_surprised = utils.merge_values(this.expression_eyebrow_surprised, surprise * 0.5f);
        if (surprise > 50.0f) {
            this.expression_eye_surprised = utils.merge_values(this.expression_eye_surprised, (surprise -50.0f) * 2.0f);
        }

        f32 expression_mouth_u = this.expression_mouth_u;
        if (expression_mouth_u >= 0.0f) {
            surprise = Math.Min(surprise, expression_mouth_u * 2.0f);

            // Mouth - Convert excess a to expression_mouth_surprised
            f32 expression_mouth_a = this.expression_mouth_a;
            f32 expression_mouth_o = this.expression_mouth_o;
            f32 expression_mouth_surprised;

            // reduce a face
            expression_mouth_surprised = Math.Min(surprise, expression_mouth_a);

            expression_mouth_a -= expression_mouth_surprised;

            // Reduce o face
            expression_mouth_o -= expression_mouth_surprised * 0.5f;
            if (expression_mouth_o < 0.0f) {
                expression_mouth_o = 0.0f;
            }

            this.expression_mouth_surprised = utils.merge_values(
                this.expression_mouth_surprised,
                expression_mouth_surprised
            );
            this.expression_mouth_a = expression_mouth_a;
            this.expression_mouth_o = expression_mouth_o;
        }
    }

    void update_anger() {
        f32 anger = this.anger;

        if (anger < 0.0f) {
            return;
        }
        
        f32 eye_max_close = Math.Max(this.expression_eye_close_left, this.expression_eye_close_right);
        if (eye_max_close == 0.0f) {
            this.expression_eye_angry = utils.merge_values(this.expression_eye_angry, anger);
        } else if (eye_max_close < 70.0f) {
            this.expression_eye_angry = utils.merge_values(
                this.expression_eye_angry,
                anger * (1.0f - (eye_max_close / 70.0f))
            );
        }

        anger = anger * 0.5f;
        this.expression_eyebrow_angry = utils.merge_values(this.expression_eyebrow_angry, anger);
        this.expression_mouth_angry = utils.merge_values(this.expression_mouth_angry, anger);
    }


    void update_sadness() {
        f32 sadness = this.sadness;

        if (sadness <= 0.0f) {
            return;
        }

        sadness = sadness * 0.5f;

        this.expression_eyebrow_angry = utils.merge_values(
            this.expression_eyebrow_angry, sadness
        );
        this.expression_eyebrow_fun = utils.merge_values(
            this.expression_eyebrow_fun, sadness
        );
        this.expression_eyebrow_joy = utils.merge_values(
            this.expression_eyebrow_joy, sadness
        );
        this.expression_eyebrow_sorrow = utils.merge_values(
            this.expression_eyebrow_sorrow, sadness
        );
        this.expression_eye_spread = utils.merge_values(
            this.expression_eye_spread, sadness
        );

        this.expression_eye_sorrow = utils.merge_values(
            this.expression_eye_sorrow, sadness
        );
        this.expression_mouth_angry = utils.merge_values(
            this.expression_mouth_angry, sadness
        );
        this.expression_mouth_neutral = utils.merge_values(
            this.expression_mouth_neutral, sadness
        );
    }


    void update_fear() {
        f32 fear = this.fear;

        if (fear <= 0.0) {
            return;
        }

        this.expression_eyebrow_sorrow = utils.merge_values(
            this.expression_eyebrow_sorrow, fear * 0.5f
        );
        this.expression_eyebrow_surprised = utils.merge_values(
            this.expression_eyebrow_surprised, fear * 0.5f
        );

        this.expression_eye_surprised = utils.merge_values(
            this.expression_eye_surprised, fear
        );
        this.expression_eye_spread = utils.merge_values(
            this.expression_eye_spread, fear * -0.5f
        );
        this.expression_eye_highlight_hide = utils.merge_values(
            this.expression_eye_highlight_hide, fear
        );

        f32 mouth_openness = (f32)Math.Sqrt((f64)(
            utils.square(this.expression_mouth_a) +
            utils.square(this.expression_mouth_u) +
            utils.square(this.expression_mouth_o) +
            utils.square(this.expression_mouth_joy) +
            utils.square(this.expression_mouth_surprised)
        )) / 2.0f;

        if (mouth_openness < 0.0f) {
            return;
        }

        if (mouth_openness < fear) {
            fear = mouth_openness;
        }

        if (fear > 75.0f) {
            fear = 75.0f;
        }

        f32 mouth_openness_reduction = 1.0f - fear / 300.0f;

        this.expression_mouth_a *= mouth_openness_reduction;
        this.expression_mouth_u *= mouth_openness_reduction;
        this.expression_mouth_o *= mouth_openness_reduction;
        this.expression_mouth_joy *= mouth_openness_reduction;
        this.expression_mouth_surprised *= mouth_openness_reduction;


        this.expression_eye_sorrow = utils.merge_values(
            this.expression_eye_sorrow, fear
        );
        this.expression_mouth_surprised = utils.merge_values(
            this.expression_mouth_surprised, fear / 3.0f
        );
    }

    void update_disgust() {
        f32 disgust = this.disgust;
        if (disgust <= 0.0f) {
            return;
        }

        this.expression_eyebrow_joy = utils.merge_values(
            this.expression_eyebrow_joy, disgust * 0.5f
        );
        this.expression_eyebrow_sorrow = utils.merge_values(
            this.expression_eyebrow_sorrow, disgust * 0.5f
        );

        this.expression_eye_sorrow = utils.merge_values(
            this.expression_eye_sorrow, disgust * 0.5f
        );

        this.expression_mouth_angry = utils.merge_values(
            this.expression_mouth_angry, disgust * 0.25f
        );

        f32 mouth_openness = (f32)Math.Sqrt((f64)(
            utils.square(this.expression_mouth_a) +
            utils.square(this.expression_mouth_u) +
            utils.square(this.expression_mouth_o) +
            utils.square(this.expression_mouth_joy) +
            utils.square(this.expression_mouth_surprised)
        )) / 2.0f;

        if (mouth_openness < 0.0f) {
            return;
        }

        if (mouth_openness < disgust) {
            disgust = mouth_openness;
        }

        if (disgust > 75.0f) {
            disgust = 75.0f;
        }

        f32 mouth_openness_reduction = 1.0f - disgust / 300.0f;

        this.expression_mouth_a *= mouth_openness_reduction;
        this.expression_mouth_u *= mouth_openness_reduction;
        this.expression_mouth_o *= mouth_openness_reduction;
        this.expression_mouth_joy *= mouth_openness_reduction;
        this.expression_mouth_surprised *= mouth_openness_reduction;

        this.expression_mouth_up = utils.merge_values(
            this.expression_mouth_up, disgust * (1.0f / 3.0f)
        );
        this.expression_mouth_sorrow = utils.merge_values(
            this.expression_mouth_sorrow, disgust * (2.0f / 3.0f)
        );
        this.expression_mouth_a = utils.merge_values(
            this.expression_mouth_a, disgust * (1.0f / 3.0f)
        );
        this.expression_teeth_short_top = utils.merge_values(
            this.expression_teeth_short_top, disgust * (-2.0f / 3.0f)
        );
    }

    void clear_expressions() {
        this.expression_eye_fun = 0.0f;
        this.expression_eye_joy_left = 0.0f;
        this.expression_eye_joy_right = 0.0f;
        this.expression_eye_surprised = 0.0f;
        this.expression_eye_angry = 0.0f;
        this.expression_eye_spread = 0.0f;
        this.expression_eye_sorrow = 0.0f;
        this.expression_eye_highlight_hide = 0.0f;

        this.expression_eyebrow_fun = 0.0f;
        this.expression_eyebrow_surprised = 0.0f;
        this.expression_eyebrow_angry = 0.0f;
        this.expression_eyebrow_joy = 0.0f;
        this.expression_eyebrow_sorrow = 0.0f;

        this.expression_mouth_joy = 0.0f;
        this.expression_mouth_surprised = 0.0f;
        this.expression_mouth_angry = 0.0f;
        this.expression_mouth_neutral = 0.0f;
        this.expression_mouth_up = 0.0f;
        this.expression_mouth_sorrow = 0.0f;

        this.expression_teeth_short_top = 0.0f;
    }

    void update_face() {
        SkinnedMeshRenderer face_mesh = this.face_mesh;
        if (face_mesh != null) {
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_CLOSE_LEFT, this.expression_eye_close_left);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_CLOSE_RIGHT, this.expression_eye_close_right);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_FUN, this.expression_eye_fun);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_SURPRISED, this.expression_eye_surprised);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_JOY_LEFT, this.expression_eye_joy_left);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_JOY_RIGHT, this.expression_eye_joy_right);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_ANGRY, this.expression_eye_angry);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_SPREAD, this.expression_eye_spread);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_SORROW, this.expression_eye_sorrow);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYE_HIGHLIGHT_HIDE, this.expression_eye_highlight_hide);


            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYEBROW_FUN, this.expression_eyebrow_fun);
            // At high values might cause eyebrow to go under the skin partially
            face_mesh.SetBlendShapeWeight(
                (i32)FACE_MESH.EYEBROW_SURPRISED, Math.Min(this.expression_eyebrow_surprised, 140.0f)
            );
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYEBROW_ANGRY, this.expression_eyebrow_angry);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYEBROW_JOY, this.expression_eyebrow_joy);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.EYEBROW_SORROW, this.expression_eyebrow_sorrow);


            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_A, this.expression_mouth_a);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_I, this.expression_mouth_i);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_U, this.expression_mouth_u);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_O, this.expression_mouth_o);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_O, this.expression_mouth_o);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_FUN, this.expression_mouth_fun);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_JOY, this.expression_mouth_joy);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_SURPRISED, this.expression_mouth_surprised);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_ANGRY, this.expression_mouth_angry);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_NEUTRAL, this.expression_mouth_neutral);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_UP, this.expression_mouth_up);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.MOUTH_SORROW, this.expression_mouth_sorrow);


            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.TEETH_SHORT_BOT, this.expression_teeth_short_bot);
            face_mesh.SetBlendShapeWeight((i32)FACE_MESH.TEETH_SHORT_TOP, this.expression_teeth_short_top);
        }
    }


    // Update is called once per frame
    void Update() {
        // Readjust as requested.
        this.maybe_readjust();
        
        // Pull them at the start in case of race condition happens.
        HeadMovementData head_movement_target = this.head_movement_target;
        ExpressionData expression_target = this.expression_target;
        BodyMovementData body_movement_target = this.body_movement_target;
    
        this.do_head_movement_smooth_step(head_movement_target);
        this.do_expression_smooth_step(expression_target);
        this.do_body_movement_smooth_step(body_movement_target);

        this.clear_expressions();
        this.update_body();
        this.update_head();
        this.update_irises();
        this.update_arms();

        // Face
        this.set_eye_expressions(head_movement_target);
        this.set_mouth_expressions();
        this.set_eyebrow_expressions();

        this.update_happiness();
        this.update_surprised();
        this.update_anger();
        this.update_sadness();
        this.update_fear();
        this.update_disgust();

        this.update_face();

        // Body
        this.update_position();
    }

    void FixedUpdate() {
        this.handle_key_presses();
    }

    void handle_key_presses() {
        if (Input.GetKeyDown(KeyCode.F5)) {
            this.invoke_adjustments();
        }
    }
    // Teardown
    
    void OnApplicationQuit() {
        TcpClient tcp_client = this.tcp_client;
        if (tcp_client != null) {
            try {
                tcp_client.Close();
            } catch (Exception exception) {
                Debug.Log(exception.Message);
            }
        }
        
        TcpListener tcp_listener = this.tcp_listener;
        if (tcp_listener != null) {
            try {
                tcp_listener.Stop();
            } catch (Exception exception) {
                Debug.Log(exception.Message);
            }
        }
    }
}
