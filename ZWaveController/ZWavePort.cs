
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Collections;

namespace ZWave
{
    class ZWavePort
    {
        //
        const int REQUEST =											0x00;
        const int RESPONSE =										0x01;
        //
        const int NUM_NODE_BITFIELD_BYTES = 29;		// 29 bytes = 232 bits, one for each possible node in the network.
        //
        const int FUNC_ID_SERIAL_API_GET_INIT_DATA = 0x02;
        const int FUNC_ID_SERIAL_API_APPL_NODE_INFORMATION = 0x03;
        const int FUNC_ID_APPLICATION_COMMAND_HANDLER = 0x04;
        const int FUNC_ID_ZW_GET_CONTROLLER_CAPABILITIES = 			0x05;
        const int  FUNC_ID_SERIAL_API_SET_TIMEOUTS 		=		0x06;
        const int FUNC_ID_SERIAL_API_GET_CAPABILITIES	=			0x07;
        const int FUNC_ID_SERIAL_API_SOFT_RESET       =					0x08;
        const int FUNC_ID_ZW_APPLICATION_UPDATE		=			0x49;	// Get a list of supported (and controller) command classes
        //
        const int FUNC_ID_ZW_SEND_DATA = 0x13;
        const int FUNC_ID_ZW_MEMORY_GET_ID = 0x20;
        const int FUNC_ID_ZW_GET_VERSION = 0x15;
        //
        private SerialPort sp;
        private Thread receiverThread;
        private Boolean sendACK = true;
        //
        const int SOF = 0x01; // SOF start of frame
        const int ACK = 0x06;
        int NAK = 0x15;
        int CAN = 0x18;
        //
        byte[] MSG_ACKNOWLEDGE = new byte[] { ACK };
        //
        private int COMMAND_CLASS_BASIC = 0x20;
        private int COMMAND_CLASS_BATTERY = 0x80;
        //
        private int COMMAND_CLASS_WAKE_UP = 0x84;
        private int COMMAND_CLASS_CONTROLLER_REPLICATION = 0x21;
        private int COMMAND_CLASS_SWITCH_MULTILEVEL = 0x26;
        private int COMMAND_CLASS_SWITCH_ALL = 0x27;
        private int COMMAND_CLASS_SENSOR_BINARY = 0x30;
        private int COMMAND_CLASS_ALARM = 0x71;
        private int COMMAND_CLASS_MULTI_CMD = 0x8F;
        private int COMMAND_CLASS_CLIMATE_CONTROL_SCHEDULE = 0x46;
        private int COMMAND_CLASS_CLOCK = 0x81;
        private int COMMAND_CLASS_ASSOCIATION = 0x85;
        private int COMMAND_CLASS_CONFIGURATION = 0x70;
        private int COMMAND_CLASS_MANUFACTURER_SPECIFIC = 0x72;

private int COMMAND_CLASS_SCENE_ACTIVATION = 0x2B;
private int COMMAND_CLASS_SCENE_ACTUATOR_CONF = 0x2C;

private int COMMAND_CLASS_VERSION = 0x86;
private int COMMAND_CLASS_MANUFACTURER_PROPRIETARY = 0x91;
private int COMMAND_CLASS_NODE_NAMING = 0x77;
private int COMMAND_CLASS_POWERLEVEL = 0x73;
private int COMMAND_CLASS_MARK = 0xEF;
private int COMMAND_CLASS_HAIL = 0x82;
private int COMMAND_CLASS_MULTI_INSTANCE = 0x60;
private int COMMAND_CLASS_SENSOR_MULTILEVEL = 0x31;
private int COMMAND_CLASS_SWITCH_BINARY = 0x25;

        //
        public ZWavePort()
        {
            sp = new SerialPort();
            sp.PortName = "COM3";
            sp.BaudRate = 115200;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Handshake = Handshake.None;
            sp.DtrEnable = true;
            sp.RtsEnable = true;
            sp.NewLine = System.Environment.NewLine;

            receiverThread = new Thread(
                new System.Threading.ThreadStart(ReceiveMessage));
        }

        public void Open()
        {
            if (sp.IsOpen == false)
            {
                sp.Open();
                receiverThread.Start();
            }
        }

        public void SendMessage(byte[] message)
        {
            if (sp.IsOpen == true)
            {
                if (message != MSG_ACKNOWLEDGE)
                {
                    sendACK = false;
                    message[message.Length - 1] = GenerateChecksum(message); // Insert checksum
                }
                System.Console.WriteLine("Message sent: " + ByteArrayToString(message));
                sp.Write(message, 0, message.Length);
            }
        }

        private void SendACKMessage()
        {
            SendMessage(MSG_ACKNOWLEDGE);
        }

        public void Close()
        {
            if (sp.IsOpen == true)
            {
                sp.Close();
            }
        }

        private static byte GenerateChecksum(byte[] data)
        {
            int offset = 1;
            byte ret = data[offset];
            for (int i = offset +1; i < data.Length - 1; i++)
            {
                // Xor bytes
                ret ^= data[i];
            }
            // Not result
            ret = (byte)(~ret);
            return ret;
        }

        private void ReceiveMessage()
        {
            //
            byte[] MessageReadArray = { 0x0 };
            //
            while (sp.IsOpen == true)
            {
                int bytesToRead = sp.BytesToRead;
                if ((bytesToRead != 0) & (sp.IsOpen == true))
                {
                    byte[] message = new byte[bytesToRead];
                    sp.Read(message, 0, bytesToRead);                    
                    //
                    System.Console.WriteLine("Message received: " + ByteArrayToString(message));
                    //
                    switch (Convert.ToInt32(message[0]))
                    {
                        //
                        case SOF:
                            //
                            System.Console.WriteLine(" *** SOF *** ");
                            //
                            System.Console.WriteLine("Message received before (Added): " + ByteArrayToString(MessageReadArray));                         
                            //
                            Array.Resize(ref MessageReadArray, 0);
                            Array.Clear(MessageReadArray, 0, MessageReadArray.Length);
                            //
                            MessageReadArray = (MessageReadArray.Concat(message)).ToArray<byte>() ;
                            //
                            System.Console.WriteLine("Message received new (Added): " + ByteArrayToString(MessageReadArray));
                            //
                            break;
                        case ACK:
                            break;
                        default:
                            //
                            //System.Console.WriteLine("Message received (Added): " + ByteArrayToString(MessageReadArray));
                            //
                            MessageReadArray = (MessageReadArray.Concat(message)).ToArray<byte>();
                            break;
                    }
                    //
                    if (MessageReadArray.Length > 2)
                    {
                        int lenght = MessageReadArray[1];
                        System.Console.WriteLine(" Length = " + lenght);
                        byte[] ReducedArray = new byte[MessageReadArray.Length - 1];
                        Array.Copy(MessageReadArray, ReducedArray, MessageReadArray.Length - 1);
                        System.Console.WriteLine(ByteArrayToString(MessageReadArray) + " CRC = " + GenerateChecksum(MessageReadArray) + " L= " + MessageReadArray[MessageReadArray.Length - 1]);
                        if (GenerateChecksum(MessageReadArray) == MessageReadArray[MessageReadArray.Length - 1])
                        {
                            int nodeId = NodeFromMessage(MessageReadArray);
                            if (nodeId == 0)
                            {
                                nodeId = GetNodeNumber(MessageReadArray);
                            }
                            System.Console.WriteLine(" Found Checksum ");
                            System.Console.WriteLine(" NodeId "+nodeId);

                            // Process the received message
                            byte[] OnlyMessage = new byte[MessageReadArray.Length-2];
                            Array.Copy(MessageReadArray, 2, OnlyMessage,0, MessageReadArray.Length-2);  
                            ProcessMsg(OnlyMessage);

                            // Reset Array
                            Array.Resize(ref MessageReadArray, 0);
                            Array.Clear(MessageReadArray, 0, MessageReadArray.Length);
                        }
                    }
                    //
                    if (sendACK) // Does the incoming message require an ACK?
                    //
                    {
                        SendACKMessage();
                    }
                    sendACK = true;
                }
            }
        }

        private  void ProcessMsg(byte[] p)
        {
            //throw new NotImplementedException();
            if ( p[0] == REQUEST)
            {
                System.Console.WriteLine("This is a request");
            } else if (p[0] == RESPONSE)
            {
                System.Console.WriteLine("This is a response with code {0:x}",p[1]);
                //
                switch(p[1])
                {
                    case FUNC_ID_ZW_SEND_DATA:
                        break;
                    case FUNC_ID_ZW_MEMORY_GET_ID:
                        MemoryGetIdResponse(p);
                        break;
                    case FUNC_ID_ZW_GET_VERSION:
                        GetVersionResponse(p);
                        break;
                    case FUNC_ID_SERIAL_API_GET_INIT_DATA:
                        SerialAPIGetInitDataResponse(p);
                        break;
                    default:
                        System.Console.WriteLine("Not implemented yet");
                        break;
                        //
                }
                //
            }
        }

        private void SerialAPIGetInitDataResponse(byte[] _data)
        {
            int m_initVersion = _data[2];
            int m_initCaps = _data[3];
            //throw new NotImplementedException();
            if (_data[4] == NUM_NODE_BITFIELD_BYTES)
            {
                //
                System.Console.WriteLine(" NUM_MODE ");
                //
                for (int i = 0; i < NUM_NODE_BITFIELD_BYTES; ++i)
                {
                    for (int j = 0; j < 8; ++j)
                    {
                        //
                        int nodeId = (i * 8) + j + 1;
                        if ( (_data[i + 5] & (0x01 << j))>0)
                        {
                            System.Console.WriteLine(" NodeID " + nodeId);
                        }
                        //
                    }
                }
            }
        }
        //
        private void GetVersionResponse(byte[] _data)
        {
            //throw new NotImplementedException();
        }
        //
        private void MemoryGetIdResponse(byte[] _data)
        {
            //throw new NotImplementedException();
            byte[] m_homeId = new byte[] { _data[2], _data[3], _data[4], _data[5] };
            //
            int m_Controller_nodeId = _data[6];
            //
            System.Console.WriteLine("HomeId " + ByteArrayToString(m_homeId));
            System.Console.WriteLine("Controller node Id " + m_Controller_nodeId);
            //
        }
        //
        private int GetNodeNumber(object m_currentMsg)
        {
            //throw new NotImplementedException();
            return 0;
        }

        private int NodeFromMessage(byte[] messageReadArray)
        {
            int nodeId = 0;
            //
            if (messageReadArray[1] >= 5)
            {
                switch (messageReadArray[3])
                {
                    case FUNC_ID_APPLICATION_COMMAND_HANDLER: nodeId = messageReadArray[5]; break;
                    case FUNC_ID_ZW_APPLICATION_UPDATE: nodeId = messageReadArray[5]; break;
                }
            }
            return nodeId;
        }
        //
        private String ByteArrayToString(byte[] message)
        {
            String ret = String.Empty;
            foreach (byte b in message)
            {
                ret += b.ToString("X2") + " ";
            }
            return ret.Trim();
        }
    }
}
