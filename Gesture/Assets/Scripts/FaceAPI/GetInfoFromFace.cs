using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInfoFromFace : MonoBehaviour
{
    [System.Serializable]
    public class Object
    {
        public string error_code;
        public string error_msg;
        public string log_id;
        public string timestamp;
        public string cached;
        public Result result;
    }

    [System.Serializable]
    public class Result
    {
        public string face_token;
        public User_List user_list;
    }
    
    [System.Serializable]
    public class User_List
    {
        public User[] user;
    }

    [System.Serializable]
    public class User
    {
        public string group_id;
        public string user_id;
        public string user_info;
        public float score;
    }



}
