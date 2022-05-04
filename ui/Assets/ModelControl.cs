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

public static class UTILS {
    public static float square(float value) {
        return value * value;
    }
    
    public static float point_difference(Vector3 point_1, Vector3 point_2) {
        return (float)Math.Sqrt((double)(
            UTILS.square(point_1.x - point_2.x) +
            UTILS.square(point_1.y - point_2.y) +
            UTILS.square(point_1.z - point_2.z)
        ));
    }
    
    public static float point_difference_2d(float point_1_x, float point_1_y, float point_2_x, float point_2_y) {
        return (float)Math.Sqrt((double)(
            UTILS.square(point_1_x - point_2_x) +
            UTILS.square(point_1_y - point_2_y)
        ));
    }
}

public class ModelControl : MonoBehaviour {
    [Header("Avatar")]
    [SerializeField]
    public Avatar avatar;

    [Header("Camera")]
    [SerializeField]
    [Range(-15.0f, +15.0f)]
    public float camera_angle_vertical = -15.0f;

    [Header("Arms")]
    [SerializeField]
    [Range(-90.0f, 0.0f)]
    public float right_arm_angle = -60.0f;

    [SerializeField]
    [Range(0.0f, 90.0f)]
    public float left_arm_angle = +60.0f;

    [Header("Mouth")]
    [SerializeField]
    [Range(50.0f, 80.0f)]
    public float default_mouth_openness_horizontal = +73.0f;

    [SerializeField]
    [Range(10.0f, 40.0f)]
    public float default_mouth_openness_vertical = +26.0f;

    [Header("Position")]
    [SerializeField]
    [Range(-10.0f, 10.0f)]
    public float default_position_deepness = 0.7f;
    [SerializeField]
    public bool lock_position_deepness = true;

    [SerializeField]
    public bool enable_position_change = true;

    [Header("Smoothing")]
    [SerializeField]
    [Range(1, 6)]
    public int smooth_level = 4;
    private int smooth_step = 0;


    // body parts
    private GameObject eye_right = null;
    private GameObject eye_left = null;

    private GameObject chest_upper = null;
    private GameObject neck = null;
    private GameObject head = null;

    private GameObject shoulder_right = null;
    private GameObject shoulder_left = null;
    private GameObject arm_upper_right = null;
    private GameObject arm_upper_left = null;
    private GameObject arm_lower_right = null;
    private GameObject arm_lower_left = null;
    private float shoulder_length = 0.0f;
    private GameObject body_root = null;
    private GameObject hips = null;

    private SkinnedMeshRenderer face_mesh = null;

    // Thread
    private Thread receiver_thread = null;
    private TcpListener tcp_listener = null;
    private TcpClient tcp_client = null;
    
    // Connection
    private int port = 5050;
    private string address = "127.0.0.1";
    
    // Fields
    [Header("Data")]
    [Range(-16.0f, +8.0f)]
    public float iris_left_x = 0.0f;
    [Range(-10.0f, +10.0f)]
    public float iris_left_y = 0.0f;
    [Range(-8.0f, +16.0f)]
    public float iris_right_x = 0.0f;
    [Range(-10.0f, +10.0f)]
    public float iris_right_y = 0.0f;

    [Range(+0.0f, +100.0f)]
    public float eye_openness_left = 0.0f;
    [Range(+0.0f, 100.0f)]
    public float eye_openness_right = 0.0f;

    [Range(-90.0f, +90.0f)]
    public float head_x = 0.0f;
    [Range(-60.0f, +30.0f)]
    public float head_y = 0.0f;
    [Range(-90.0f, +90.0f)]
    public float head_z = 0.0f;

    [Range(-0.0f, +100.0f)]
    public float mouth_openness_x = 0.0f;
    [Range(-0.0f, +100.0f)]
    public float mouth_openness_y = 0.0f;

    [Range(-5.0f, +5.0f)]
    public float face_position_x = 0.0f;
    [Range(-5.0f, +5.0f)]
    public float face_position_y = 0.0f;
    [Range(-10.0f, -1.0f)]
    public float face_position_z = 0.0f;

    [Range(0.0f, 100.0f)]
    public float smile_ratio = 0.0f;
    [Range(0.0f, 100.0f)]
    public float eyebrow_liftedness = 0.0f;


    private float target_iris_left_x = 0.0f;
    private float target_iris_left_y = 0.0f;
    private float target_iris_right_x = 0.0f;
    private float target_iris_right_y = 0.0f;
    private float target_eye_openness_left = 0.0f;
    private float target_eye_openness_right = 0.0f;
    private float target_head_x = 0.0f;
    private float target_head_y = 0.0f;
    private float target_head_z = 0.0f;
    private float target_mouth_openness_x = 0.0f;
    private float target_mouth_openness_y = 0.0f;
    private float target_face_position_x = 0.0f;
    private float target_face_position_y = 0.0f;
    private float target_face_position_z = 0.0f;
    private float target_smile_ratio = 0.0f;
    private float target_eyebrow_liftedness = 0.0f;

    private float step_iris_left_x = 0.0f;
    private float step_iris_left_y = 0.0f;
    private float step_iris_right_x = 0.0f;
    private float step_iris_right_y = 0.0f;
    private float step_eye_openness_left = 0.0f;
    private float step_eye_openness_right = 0.0f;
    private float step_head_x = 0.0f;
    private float step_head_y = 0.0f;
    private float step_head_z = 0.0f;
    private float step_mouth_openness_x = 0.0f;
    private float step_mouth_openness_y = 0.0f;
    private float step_face_position_x = 0.0f;
    private float step_face_position_y = 0.0f;
    private float step_face_position_z = 0.0f;
    private float step_smile_ratio = 0.0f;
    private float step_eyebrow_liftedness = 0.0f;

    // eye close
    private bool is_left_eye_closing = true;
    private bool is_right_eye_closing = true;

    // system
    /* Unused for now */
    private float target_fps = 60.0f;

    /* This will be changed at startup, no worries */
    private float head_size = 1.0f;
    private Vector3 hips_position = new Vector3(0.0f, 0.0f, 0.0f);

    // Expressions
    private float expression_eye_close_left = 0.0f;
    private float expression_eye_close_right = 0.0f;
    private float expression_eye_fun = 0.0f;
    private float expression_eye_surprised = 0.0f;
    private float expression_eye_joy = 0.0f;

    private float expression_eyebrow_fun = 0.0f;
    private float expression_eyebrow_surprised = 0.0f;
    
    private float expression_mouth_a = 0.0f;
    private float expression_mouth_i = 0.0f;
    private float expression_mouth_u = 0.0f;
    private float expression_mouth_o = 0.0f;
    private float expression_mouth_fun = 0.0f;
    private float expression_mouth_surprised = 0.0f;
    private float expression_mouth_joy = 0.0f;

    private float expression_teeth_short_bot = 0.0f;

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
        int length;
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
                            length = stream.Read(payload_buffer, 0, payload_buffer.Length);
                            if (length == 0) {
                                break;
                            }
                            
                            var received_payload = new byte[length];
                            Array.Copy(payload_buffer, 0, received_payload, 0, length);
                            
                            string received_message = Encoding.ASCII.GetString(received_payload);
                            string[] floats = received_message.Split(' ');

                            this.update_positions_from_array(floats);
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

    public void update_positions_from_array(string[] floats) {
        this.target_iris_left_x = float.Parse(floats[0]);;

        this.target_iris_left_y = float.Parse(floats[1]);
        this.target_iris_right_x = float.Parse(floats[2]);

        this.target_iris_right_y = float.Parse(floats[3]);

        this.target_eye_openness_left = float.Parse(floats[4]);
        this.target_eye_openness_right = float.Parse(floats[5]);

        this.target_head_x = float.Parse(floats[6]);
        this.target_head_y = float.Parse(floats[7]) - this.camera_angle_vertical;
        this.target_head_z = float.Parse(floats[8]);

        this.target_mouth_openness_x = float.Parse(floats[9]);
        this.target_mouth_openness_y = float.Parse(floats[10]);

        this.target_face_position_y = float.Parse(floats[11]);
        this.target_face_position_x = float.Parse(floats[12]);
        this.target_face_position_z = float.Parse(floats[13]);

        this.target_smile_ratio = float.Parse(floats[14]);
        this.target_eyebrow_liftedness = float.Parse(floats[15]);

        // Reset smooth step
        this.smooth_step = 0;
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

    public IEnumerable<VRMSpringBone> iter_secondary_body_bones(string name) {
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

        // Chest
        this.chest_upper = this.get_child_with_name(BODY_PART_NAME.CHEST_UPPER);

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
        this.hips = this.get_child_with_name(BODY_PART_NAME.HIPS);
    }


    void try_adjust_colliders_of(GameObject game_object, float by) {
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

                    float collision_box_size = (
                        UTILS.point_difference(chest_left_root_position, chest_left_mid_position) +
                        UTILS.point_difference(chest_right_root_position, chest_right_mid_position)
                    );

                    VRMSpringBoneColliderGroup.SphereCollider[] new_colliders = (
                        new VRMSpringBoneColliderGroup.SphereCollider[7]{
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
    }

    void try_adjust_chest_gravity() {
        VRMSpringBone chest_bone = this.get_secondary_body_bone(SECONDARY_BODY_PART_NAME.CHEST);
        if (chest_bone != null) {
            chest_bone.m_stiffnessForce = 0.2f;
            chest_bone.m_gravityPower = 0.05f;
            chest_bone.m_dragForce = 1.0f;
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
            hair.m_stiffnessForce = 1.0f;
            hair.m_gravityPower = 0.08f;
            hair.m_dragForce = 1.0f;
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
                UTILS.point_difference(arm_upper_right.transform.position, shoulder_right.transform.position) +
                UTILS.point_difference(arm_upper_right.transform.position, shoulder_left.transform.position)
            ) * 0.5f;
        }
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

    void set_hips_position() {
        GameObject body_root = this.body_root;
        GameObject hips = this.hips;
        if ((body_root != null) && (hips != null)) {
            /* Revert the rotation */
            this.hips_position = (
                Quaternion.Inverse(hips.transform.rotation) * hips.transform.position -
                Quaternion.Inverse(hips.transform.rotation) * body_root.transform.position
            );
        }
    }

    // Start is called before the first frame update

    void Start() {
        this.try_set_body_parts();

        this.set_head_size();
        this.set_hips_position();

        this.try_adjust_colliders();
        this.try_adjust_chest_gravity();
        this.try_adjust_ear_cat_gravity();
        this.try_adjust_hair_gravity();

        this.maybe_start_tpc_connection();

        this.target_fps = Application.targetFrameRate;

        this.set_shoulder_length();

        // Updates

        this.update_arms();
    }
    
    // Update
    void smoothing_step() {
        int smooth_level = this.smooth_level;
        int smooth_step = this.smooth_step;
        this.smooth_step = smooth_step + 1;

        if (smooth_level == 1) {
            if (smooth_step == 0) {
                this.iris_left_x = this.target_iris_left_x;
                this.iris_left_y = this.target_iris_left_y;
                this.iris_right_x = this.target_iris_right_x;
                this.iris_right_y = this.target_iris_right_y;
                this.eye_openness_left = this.target_eye_openness_left;
                this.eye_openness_right = this.target_eye_openness_right;
                this.head_x = this.target_head_x;
                this.head_y = this.target_head_y;
                this.head_z = this.target_head_z;
                this.mouth_openness_x = this.target_mouth_openness_x;
                this.mouth_openness_y = this.target_mouth_openness_y;
                this.face_position_x = this.target_face_position_x;
                this.face_position_y = this.target_face_position_y;
                this.face_position_z = this.target_face_position_z;
                this.smile_ratio = this.target_smile_ratio;
                this.eyebrow_liftedness = this.target_eyebrow_liftedness;
            }
        } else {
            if (smooth_step == 0) {
                this.step_iris_left_x = (this.target_iris_left_x - this.iris_left_x) / smooth_level;
                this.step_iris_left_y = (this.target_iris_left_y - this.iris_left_y) / smooth_level;
                this.step_iris_right_x = (this.target_iris_right_x - this.iris_right_x) / smooth_level;
                this.step_iris_right_y = (this.target_iris_right_y - this.iris_right_y) / smooth_level;
                this.step_eye_openness_left = (
                    (this.target_eye_openness_left - this.eye_openness_left) / smooth_level
                );
                this.step_eye_openness_right = (
                    (this.target_eye_openness_right - this.eye_openness_right) / smooth_level
                );
                this.step_head_x = (this.target_head_x - this.head_x) / smooth_level;
                this.step_head_y = (this.target_head_y - this.head_y) / smooth_level;
                this.step_head_z = (this.target_head_z - this.head_z) / smooth_level;
                this.step_mouth_openness_x = (this.target_mouth_openness_x - this.mouth_openness_x) / smooth_level;
                this.step_mouth_openness_y = (this.target_mouth_openness_y - this.mouth_openness_y) / smooth_level;
                this.step_face_position_x = (this.target_face_position_x - this.face_position_x) / smooth_level;
                this.step_face_position_y = (this.target_face_position_y - this.face_position_y) / smooth_level;
                this.step_face_position_z = (this.target_face_position_z - this.face_position_z) / smooth_level;
                this.step_smile_ratio = (this.target_smile_ratio - this.smile_ratio) / smooth_level;
                this.step_eyebrow_liftedness = (this.target_eyebrow_liftedness - this.eyebrow_liftedness) / smooth_level;
            } else if (smooth_step >= smooth_level) {
                goto skip_step;
            }

            this.iris_left_x += this.step_iris_left_x;
            this.iris_left_y += this.step_iris_left_y;
            this.iris_right_x += this.step_iris_right_x;
            this.iris_right_y += this.step_iris_right_y;
            this.eye_openness_left += this.step_eye_openness_left;
            this.eye_openness_right += this.step_eye_openness_right;
            this.head_x += this.step_head_x;
            this.head_y += this.step_head_y;
            this.head_z += this.step_head_z;
            this.mouth_openness_x += this.step_mouth_openness_x;
            this.mouth_openness_y += this.step_mouth_openness_y;
            this.face_position_x += this.step_face_position_x;
            this.face_position_y += this.step_face_position_y;
            this.face_position_z += this.step_face_position_z;
            this.smile_ratio += this.step_smile_ratio;
            this.eyebrow_liftedness += this.step_eyebrow_liftedness;

            skip_step:;
        }

    }


    float within_range(float current_value, float target_value, float value_range) {
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

            eye_right.transform.rotation = base_rotation * Quaternion.Euler(this.iris_right_y, this.iris_right_x, 0.0f);
            eye_left.transform.rotation = base_rotation * Quaternion.Euler(this.iris_left_y, this.iris_left_x, 0.0f);
        }
    }
    
    void update_head() {
        /* Rotate both head and neck by half of the total head rotation. This makes it more natural. */
        GameObject chest_upper = this.chest_upper;
        GameObject neck = this.neck;
        GameObject head = this.head;

        if ((chest_upper != null) && (neck != null) && (head != null)) {
            Quaternion rotation_change = Quaternion.Euler(head_y * 0.5f, head_x * 0.5f, head_z * 0.5f);

            Quaternion rotation = chest_upper.transform.rotation * rotation_change;
            neck.transform.rotation = rotation;

            rotation = rotation * rotation_change;
            head.transform.rotation = neck.transform.rotation * rotation;
        }
    }
    
    
    void update_arms() {
        GameObject shoulder_right = this.shoulder_right;
        GameObject shoulder_left = this.shoulder_left;
        GameObject arm_upper_right = this.arm_upper_right;
        GameObject arm_upper_left = this.arm_upper_left;
        GameObject arm_lower_right = this.arm_lower_right;
        GameObject arm_lower_left = this.arm_lower_left;

        if (
            (shoulder_right != null) &&
            (shoulder_left != null) &&
            (arm_upper_right != null) &&
            (arm_upper_left != null) &&
            (arm_lower_right != null) &&
            (arm_lower_left != null)
        ) {
            Transform shoulder_right_transform = shoulder_right.transform;
            Transform shoulder_left_transform = shoulder_left.transform;
            Transform arm_upper_right_transform = arm_upper_right.transform;
            Transform arm_upper_left_transform = arm_upper_left.transform;

            float right_arm_angle = this.right_arm_angle;
            float left_arm_angle = this.left_arm_angle;

            arm_upper_right_transform.rotation = (
                shoulder_right_transform.rotation * Quaternion.Euler(0.0f, 0.0f, right_arm_angle)
            );

            arm_upper_left_transform.rotation = (
                shoulder_left_transform.rotation * Quaternion.Euler(0.0f, 0.0f, left_arm_angle)
            );

            if (right_arm_angle < -70.0f) {
                arm_lower_right.transform.rotation = (
                    arm_upper_right_transform.rotation * Quaternion.Euler(0.0f, 0.0f, (right_arm_angle + 70.0f) * -0.5f)
                );
            } else {
                arm_lower_right.transform.rotation = arm_upper_right_transform.rotation;
            }

            if (left_arm_angle > 70.0f) {
                arm_lower_left.transform.rotation = (
                    arm_upper_left_transform.rotation * Quaternion.Euler(0.0f, 0.0f, (left_arm_angle - 70.0f) * -0.5f)
                );
            } else {
                arm_lower_left.transform.rotation = arm_upper_left_transform.rotation;
            }

            arm_upper_right_transform.position = (
                shoulder_right_transform.position +
                shoulder_right_transform.rotation * new Vector3(
                    this.shoulder_length * (1.0f + Math.Abs(right_arm_angle / 180f)), 0.0f, 0.0f
                )
            );

            arm_upper_left_transform.position = (
                shoulder_left_transform.position +
                shoulder_left_transform.rotation * new Vector3(
                    -this.shoulder_length * (1.0f + Math.Abs(left_arm_angle / 180f)), 0.0f, 0.0f
                )
            );
        }
    }
    
    void update_position() {
        GameObject body_root = this.body_root;
        GameObject hips = this.hips;
        GameObject head = this.head;
        GameObject neck = this.neck;

        if ((body_root != null) && (hips != null) && (head != null) && (neck != null)) {

            Vector3 neck_difference = neck.transform.position - head.transform.position;
            Vector3 hips_position = body_root.transform.position + (hips.transform.rotation * this.hips_position);

            float head_size = this.head_size;

            float position_z;
            if (this.lock_position_deepness) {
                position_z = hips_position.z;
            } else {
                position_z = (
                    hips_position.z +
                    this.face_position_z * head_size +
                    neck_difference.z +
                    default_position_deepness
                );
            }

            hips.transform.position = new Vector3(
                hips_position.x + this.face_position_y * head_size + neck_difference.x,
                hips_position.y + this.face_position_x * head_size + neck_difference.y,
                position_z
            );
        }
    }


    void set_eye_expressions() {
        float eye_openness_left = this.target_eye_openness_left;
        float eye_openness_right = this.target_eye_openness_right;
        bool is_left_eye_closing = this.is_left_eye_closing;
        bool is_right_eye_closing = this.is_right_eye_closing;

        bool should_close_left;
        bool should_close_right;

        // Detect whether we should close the eyes
        if (
            (eye_openness_left <= 10.0f) ||
            (
                (eye_openness_left <= 40.0f) &&
                (
                    (eye_openness_right <= 10.0f) ||
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
            (eye_openness_right <= 10.0f) ||
            (
                (eye_openness_right <= 40.0f) &&
                (
                    (eye_openness_left <= 10.0f) ||
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

        float expression_eye_close_left = this.expression_eye_close_left;
        float expression_eye_close_right = this.expression_eye_close_right;

        if (should_close_left) {
            expression_eye_close_left = Math.Min(expression_eye_close_left + 10.0f, 70.0f);
        } else {
            expression_eye_close_left = Math.Max(expression_eye_close_left - 10.0f, 0.0f);
        }

        if (should_close_right) {
            expression_eye_close_right = Math.Min(expression_eye_close_right + 10.0f, 70.0f);
        } else {
            expression_eye_close_right = Math.Max(expression_eye_close_right - 10.0f, 0.0f);
        }

        this.expression_eye_close_left = expression_eye_close_left;
        this.expression_eye_close_right = expression_eye_close_right;

        // Store state
        this.is_left_eye_closing = should_close_left;
        this.is_right_eye_closing = should_close_right;
    }


    void set_mouth_expressions() {
        float mouth_x = this.mouth_openness_x - this.default_mouth_openness_horizontal;
        float mouth_y = this.mouth_openness_y - this.default_mouth_openness_vertical;
            
        float expression_mouth_a = 0.0f;
        float expression_mouth_i = 0.0f;
        float expression_mouth_u = 0.0f;
        float expression_mouth_o = 0.0f;

        if (mouth_y > 6.0f) {
            expression_mouth_a = (mouth_y - 6.0f) * 2.0f;

            if (expression_mouth_a > 100.0f) {
                expression_mouth_o = expression_mouth_a - 100.0f;
                expression_mouth_a = 100.0f;

                if (expression_mouth_o > 100.0f) {
                    expression_mouth_o = 100.0f;
                }
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
                float openness_reduction = (expression_mouth_u - 50.0f) * 0.5f;

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

        this.expression_mouth_a = expression_mouth_a;
        this.expression_mouth_i = expression_mouth_i;
        this.expression_mouth_u = expression_mouth_u;
        this.expression_mouth_o = expression_mouth_o;
        this.expression_teeth_short_bot = -expression_mouth_o;
    }


    void update_fun() {
        float smile_ratio = this.smile_ratio;

        // Cap on eye fun with blinkage
        float expression_eye_close_left = this.expression_eye_close_left;
        float expression_eye_close_right = this.expression_eye_close_right;

        float expression_eye_fun;
        if (expression_eye_close_left > expression_eye_close_right) {
            expression_eye_fun = expression_eye_close_left;
        } else {
            expression_eye_fun = expression_eye_close_right;
        }

        if (smile_ratio > expression_eye_fun) {
            expression_eye_fun = smile_ratio;
        }
        this.expression_eye_fun = expression_eye_fun;

        // Cap mouth i
        float expression_mouth_i = this.expression_mouth_i - smile_ratio;
        if (expression_mouth_i < 0.0f) {
            expression_mouth_i = 0.0f;
        }
        this.expression_mouth_i = expression_mouth_i;

        // Eyebrows
        this.expression_eyebrow_fun = smile_ratio;

        // Mouth
        this.expression_mouth_fun = smile_ratio;
    }
    
    void update_surprised() {
        float eyebrow_liftedness = this.eyebrow_liftedness;
        // Update eyebrow even if we don not do `o` face
        this.expression_eyebrow_surprised = eyebrow_liftedness;

        float expression_mouth_u = this.expression_mouth_u;
        if (expression_mouth_u <= 0.0f) {
            this.expression_eye_surprised = 0.0f;
            this.expression_mouth_surprised = 0.0f;
        } else {

            eyebrow_liftedness = Math.Min(eyebrow_liftedness, expression_mouth_u * 2.0f);

            // eye
            this.expression_eye_surprised = eyebrow_liftedness;


            // Mouth - Convert excess a to expression_mouth_surprised
            float expression_mouth_a = this.expression_mouth_a;
            float expression_mouth_o = this.expression_mouth_o;
            float expression_mouth_surprised;

            // reduce a face
            expression_mouth_surprised = Math.Min(eyebrow_liftedness, expression_mouth_a);

            expression_mouth_a -= expression_mouth_surprised;

            // Reduce o face
            expression_mouth_o -= expression_mouth_surprised * 0.5f;
            if (expression_mouth_o < 0.0f) {
                expression_mouth_o = 0.0f;
            }

            this.expression_mouth_surprised = expression_mouth_surprised;
            this.expression_mouth_a = expression_mouth_a;
            this.expression_mouth_o = expression_mouth_o;
        }
    }

    void update_joy() {
        float smile_ratio = this.smile_ratio;
        if (smile_ratio <= 0.0f) {
            this.expression_mouth_joy = 0.0f;
            this.expression_eye_joy = 0.0f;

        } else {
            float expression_mouth_a = this.expression_mouth_a;
            float expression_mouth_o = this.expression_mouth_o;
            float expression_mouth_i = this.expression_mouth_i;

            float mouth_openness = (expression_mouth_a + expression_mouth_o) / 2.0f;
            float joy = Math.Min(mouth_openness, smile_ratio * 3.0f);

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
            this.expression_mouth_joy = joy;

            float eye_openness = (this.eye_openness_left + this.eye_openness_right);
            if (eye_openness > 160.0f) {
                this.expression_eye_joy = 0.0f;
            } else {
                float eye_joy = Math.Min((160.0f - eye_openness) * 2.0f, joy * 1.5f);

                if (eye_joy > 70.0f) {
                    eye_joy = 70.0f;
                }
                
                this.expression_eye_joy = eye_joy;

                this.expression_eye_close_left = Math.Max(this.expression_eye_close_left - eye_joy, 0.0f);
                this.expression_eye_close_right = Math.Max(this.expression_eye_close_right - eye_joy, 0.0f);
            }
        }
    }

    void update_face() {
        SkinnedMeshRenderer face_mesh = this.face_mesh;
        if (face_mesh != null) {
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYE_CLOSE_LEFT, this.expression_eye_close_left);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYE_CLOSE_RIGHT, this.expression_eye_close_right);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYE_FUN, this.expression_eye_fun);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYE_SURPRISED, this.expression_eye_surprised);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYE_JOY, this.expression_eye_joy);

            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYEBROW_FUN, this.expression_eyebrow_fun);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.EYEBROW_SURPRISED, this.expression_eyebrow_surprised);

            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_A, this.expression_mouth_a);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_I, this.expression_mouth_i);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_U, this.expression_mouth_u);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_O, this.expression_mouth_o);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_O, this.expression_mouth_o);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_FUN, this.expression_mouth_fun);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_JOY, this.expression_mouth_joy);
            face_mesh.SetBlendShapeWeight((int)FACE_MESH.MOUTH_SURPRISED, this.expression_mouth_surprised);

            face_mesh.SetBlendShapeWeight((int)FACE_MESH.TEETH_SHORT_BOT, this.expression_teeth_short_bot);
        }
    }




    // Update is called once per frame
    void Update() {
        this.smoothing_step();
        this.update_head();
        this.update_irises();
        this.update_arms();
        
        // Face
        this.set_eye_expressions();
        this.set_mouth_expressions();

        this.update_fun();
        this.update_surprised();
        this.update_joy();

        this.update_face();

        // Body
        if (this.enable_position_change) {
            this.update_position();
        }
    }
    
    // Teardown
    
    void OnApplicationQuit() {
        TcpClient tcp_client = this.tcp_client;
        if (tcp_client != null) {
            try {
                tcp_client.Close();
            } catch(Exception e) {
                UnityEngine.Debug.Log(e.Message);
            }
        }
        
        TcpListener tcp_listener = this.tcp_listener;
        if (tcp_listener != null) {
            try {
                tcp_listener.Stop();
            } catch(Exception e) {
                UnityEngine.Debug.Log(e.Message);
            }
        }
    }
}
