
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
        private SerialPort sp;
        private Thread receiverThread;
        private Boolean sendACK = true;
        //
        const int SOF = 0x01; // SOF start of frame
        static byte ACK = 0x06;
        int NAK = 0x15;
        int CAN = 0x18;
        //
        byte[] MSG_ACKNOWLEDGE = new byte[] { ACK };
        //
        private int COMMAND_CLASS_BASIC = 0x20;
        private int COMMAND_CLASS_BATTERY = 0x80;

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
            for (int i = offset + 1; i < data.Length - 1; i++)
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
                    byte[] MessageReadArray;
                    //
                    switch (Convert.ToInt32(message[0]))
                    {
                        case SOF:
                            //
                            System.Console.WriteLine("Message received (Added): " + ByteArrayToString(MessageReadArray));
                            //
                            System.Console.WriteLine(" SOF ");
                            //
                            break;
                        default:
                            ReadArray.Add(message);
                            break;
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
