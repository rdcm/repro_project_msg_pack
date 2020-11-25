using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

public class SenderScript : MonoBehaviour
{
    private static bool _serializerRegistered = false;
    private UdpClient _client;
    private IPEndPoint _serverIp;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!_serializerRegistered)
        {
            StaticCompositeResolver.Instance.Register(
                GeneratedResolver.Instance,
                StandardResolver.Instance);

            var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = option;
            _serializerRegistered = true;
        }
        
        _client = new UdpClient();
        _serverIp = new IPEndPoint(IPAddress.Loopback, 7777);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Send event");
            var joinEvent = new JoinEvent
            {
                RoomId = 123,
                Nickname = "foo"
            };

            var bytes = MessagePackSerializer.Serialize(joinEvent);

            var s = string.Empty;
            foreach (var b in bytes)
            {
                s += " ";
                s += b;
            }

            Debug.Log($"Bytes sended {s}");
            _client.Send(bytes, bytes.Length, _serverIp);
        }
    }
}
