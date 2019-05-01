using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class GetInfoFromFace : MonoBehaviour
{

    public class SearchFaceMatchInfo
    {
        public JObject OriginInfo;
        public string error_code;
        public string error_msg;
        public string log_id;
        public string timestamp;
        public string cached;
        public string face_token;
        public string group_id;
        public string user_id;
        public string user_info;
        public float score;
    }

    public SearchFaceMatchInfo info;


    // Use this for initialization
    void Start()
    {
        info = new SearchFaceMatchInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float ParseJson(JObject js)
    {

        SearchFaceMatchInfo SearchFaceMatchInfo = new SearchFaceMatchInfo();

        SearchFaceMatchInfo.log_id = js.Value<string>("log_id");//log_id
        SearchFaceMatchInfo.timestamp = js.Value<string>("timestamp");//timestamp
        SearchFaceMatchInfo.cached = js.Value<string>("cached");//cached
        SearchFaceMatchInfo.cached = js["result"]["face_token"].ToString();//face_token

        JArray res = js["result"].Value<JArray>("user_list");
        JObject j = JObject.Parse(res[0].ToString());
        SearchFaceMatchInfo.group_id = j.Value<string>("group_id");//group_id
        SearchFaceMatchInfo.user_id = j.Value<string>("user_id");//user_id
        SearchFaceMatchInfo.user_info = j.Value<string>("user_info");//user_info
        SearchFaceMatchInfo.score = j.Value<float>("score");//score
        return SearchFaceMatchInfo.score;
    }
}
