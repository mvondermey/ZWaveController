using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * public enum ControllerMessageType {
    SerialApiGetInitData(0x02,"SerialApiGetInitData"),									// Request initial information about devices in network
    SerialApiApplicationNodeInfo(0x03,"SerialApiApplicationNodeInfo"),					// Set controller node information
    ApplicationCommandHandler(0x04,"ApplicationCommandHandler"),						// Handle application command
    GetControllerCapabilities(0x05,"GetControllerCapabilities"),						// Request controller capabilities (primary role, SUC/SIS availability)
    SerialApiSetTimeouts(0x06,"SerialApiSetTimeouts"),									// Set Serial API timeouts
    GetCapabilities(0x07,"GetCapabilities"),							                // Request Serial API capabilities from the controller
    SerialApiSoftReset(0x08,"SerialApiSoftReset"),										// Soft reset. Restarts Z-Wave chip
    RfReceiveMode(0x10,"RfReceiveMode"),												// Power down the RF section of the stick
    SetSleepMode(0x11,"SetSleepMode"),													// Set the CPU into sleep mode
    SendNodeInfo(0x12,"SendNodeInfo"),													// Send Node Information Frame of the stick
    SendData(0x13,"SendData"),															// Send data.
    SendDataMulti(0x14, "SendDataMulti"),
    GetVersion(0x15,"GetVersion"),														// Request controller hardware version
    SendDataAbort(0x16,"SendDataAbort"),												// Abort Send data.
    RfPowerLevelSet(0x17,"RfPowerLevelSet"),											// Set RF Power level
    SendDataMeta(0x18, "SendDataMeta"),
    GetRandom(0x1c,"GetRandom"),														// ???
    MemoryGetId(0x20,"MemoryGetId"),													// ???
    MemoryGetByte(0x21,"MemoryGetByte"),												// Get a byte of memory.
    MemoryPutByte(0x22, "MemoryPutByte"),
    ReadMemory(0x23,"ReadMemory"),														// Read memory.
    WriteMemory(0x24, "WriteMemory"),
    SetLearnNodeState(0x40,"SetLearnNodeState"),    									// ???
    IdentifyNode(0x41,"IdentifyNode"),    												// Get protocol info (baud rate, listening, etc.) for a given node
    SetDefault(0x42,"SetDefault"),    													// Reset controller and node info to default (original) values
    NewController(0x43,"NewController"),												// ???
    ReplicationCommandComplete(0x44,"ReplicationCommandComplete"),						// Replication send data complete
    ReplicationSendData(0x45,"ReplicationSendData"),									// Replication send data
    AssignReturnRoute(0x46,"AssignReturnRoute"),										// Assign a return route from the specified node to the controller
    DeleteReturnRoute(0x47,"DeleteReturnRoute"),										// Delete all return routes from the specified node
    RequestNodeNeighborUpdate(0x48,"RequestNodeNeighborUpdate"),						// Ask the specified node to update its neighbors (then read them from the controller)
    ApplicationUpdate(0x49,"ApplicationUpdate"),										// Get a list of supported (and controller) command classes
    AddNodeToNetwork(0x4a,"AddNodeToNetwork"),											// Control the addnode (or addcontroller) process...start, stop, etc.
    RemoveNodeFromNetwork(0x4b,"RemoveNodeFromNetwork"),								// Control the removenode (or removecontroller) process...start, stop, etc.
    CreateNewPrimary(0x4c,"CreateNewPrimary"),											// Control the createnewprimary process...start, stop, etc.
    ControllerChange(0x4d,"ControllerChange"),    										// Control the transferprimary process...start, stop, etc.
    SetLearnMode(0x50,"SetLearnMode"),													// Put a controller into learn mode for replication/ receipt of configuration info
    AssignSucReturnRoute(0x51,"AssignSucReturnRoute"),									// Assign a return route to the SUC
    EnableSuc(0x52,"EnableSuc"),														// Make a controller a Static Update Controller
    RequestNetworkUpdate(0x53,"RequestNetworkUpdate"),									// Network update for a SUC(?)
    SetSucNodeID(0x54,"SetSucNodeID"),													// Identify a Static Update Controller node id
    DeleteSUCReturnRoute(0x55,"DeleteSUCReturnRoute"),									// Remove return routes to the SUC
    GetSucNodeId(0x56,"GetSucNodeId"),													// Try to retrieve a Static Update Controller node id (zero if no SUC present)
    SendSucId(0x57, "SendSucId"),
    RequestNodeNeighborUpdateOptions(0x5a,"RequestNodeNeighborUpdateOptions"),   		// Allow options for request node neighbor update
    RequestNodeInfo(0x60,"RequestNodeInfo"),											// Get info (supported command classes) for the specified node
    RemoveFailedNodeID(0x61,"RemoveFailedNodeID"),										// Mark a specified node id as failed
    IsFailedNodeID(0x62,"IsFailedNodeID"),												// Check to see if a specified node has failed
    ReplaceFailedNode(0x63,"ReplaceFailedNode"),										// Remove a failed node from the controller's list (?)
    GetRoutingInfo(0x80,"GetRoutingInfo"),												// Get a specified node's neighbor information from the controller
    LockRoute(0x90, "LockRoute"),
    SerialApiSlaveNodeInfo(0xA0,"SerialApiSlaveNodeInfo"),								// Set application virtual slave node information
    ApplicationSlaveCommandHandler(0xA1,"ApplicationSlaveCommandHandler"),				// Slave command handler
    SendSlaveNodeInfo(0xA2,"ApplicationSlaveCommandHandler"),							// Send a slave node information frame
    SendSlaveData(0xA3,"SendSlaveData"),												// Send data from slave
    SetSlaveLearnMode(0xA4,"SetSlaveLearnMode"),										// Enter slave learn mode
    GetVirtualNodes(0xA5,"GetVirtualNodes"),											// Return all virtual nodes
    IsVirtualNode(0xA6,"IsVirtualNode"),												// Virtual node test
    WatchDogEnable(0xB6, "WatchDogEnable"),
    WatchDogDisable(0xB7, "WatchDogDisable"),
    WatchDogKick(0xB6, "WatchDogKick"),
    RfPowerLevelGet(0xBA,"RfPowerLevelSet"),											// Get RF Power level
    GetLibraryType(0xBD, "GetLibraryType"),												// Gets the type of ZWave library on the stick
    SendTestFrame(0xBE, "SendTestFrame"),												// Send a test frame to a node
    GetProtocolStatus(0xBF, "GetProtocolStatus"),
    SetPromiscuousMode(0xD0,"SetPromiscuousMode"), 										// Set controller into promiscuous mode to listen to all frames
    PromiscuousApplicationCommandHandler(0xD1,"PromiscuousApplicationCommandHandler"),
    ALL(-1, null);
 */

/*
 * Reloading interface "Z-Wave 1.0.350"
Starting interface "Z-Wave 1.0.350" (pid 351)
Started interface "Z-Wave 1.0.350"
Z-Wave Debug starting serial connection loop
Z-Wave Debug starting zwave packet parser thread
Z-Wave Debug SENT getVersion: 01 03 00 15 E9
Z-Wave Debug RCVD getVersion: 01 10 01 15 5A 2D 57 61 76 65 20 33 2E 39 35 00 01 99
Z-Wave Debug . . getVersion: Z-Wave 3.95 static controller
Z-Wave Debug SENT getHomeID: 01 03 00 20 DC
Z-Wave Debug RCVD getHomeID: 01 08 01 20 F5 63 88 3A 01 F3
Z-Wave Debug . . getHomeID: 4116940858, nodeID: 001
Z-Wave Debug SENT getInterfaceFeatures: 01 03 00 05 F9
Z-Wave Debug RCVD getInterfaceFeatures: 01 04 01 05 08 F7
Z-Wave Debug . . getInterfaceFeatures: 08
Z-Wave Debug SENT getInterfaceApiCapabilities: 01 03 00 07 FB
Z-Wave Debug RCVD getInterfaceApiCapabilities: 01 2B 01 07 01 00 00 86 00 01 00 5A FE 81 FF 88 4F 1F 00 00 FB 9F 7D A0 67 00 00 80 00 80 86 00 00 00 E8 73 00 00 0E 00 00 60 00 00 FB
Z-Wave Debug . . getInterfaceApiCapabilities: serialApi 1.00, manufactureId 0086, productType 0001, productId 005A
Z-Wave connected to Z-Wave 3.95 static controller interface on /dev/cu.usbmodem1431 (firmware 1.00)
Z-Wave Debug SENT getInitialNodeData: 01 03 00 02 FE
Z-Wave Debug RCVD getInitialNodeData: 01 25 01 02 05 00 1D 1F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 05 00 DB
Z-Wave Debug . . getInitialNodeData: [1, 2, 3, 4, 5]
Z-Wave Debug SENT getNodeProtocolInfo: 01 04 00 41 01 BB
Z-Wave Debug RCVD getNodeProtocolInfo: 01 09 01 41 93 16 01 02 02 01 33
Z-Wave Debug . . getNodeProtocolInfo: node 001, class name: Static Computer Controller
Z-Wave Debug . . getNodeProtocolInfo: class hierarchy: Static Controller : Static Controller : Static Computer Controller (02 : 02 : 01)
Z-Wave Debug . . getNodeProtocolInfo: base class command: 00
Z-Wave Debug . . getNodeProtocolInfo: supported class commands: 20
Z-Wave Debug . . getNodeProtocolInfo: features: beaming
Z-Wave Debug SENT getNodeProtocolInfo: 01 04 00 41 02 B8
Z-Wave Debug RCVD getNodeProtocolInfo: 01 09 01 41 53 9C 00 04 20 01 5C
Z-Wave Debug . . getNodeProtocolInfo: node 002, class name: Binary Sensor (routing)
Z-Wave Debug . . getNodeProtocolInfo: class hierarchy: Routing Slave : Binary Sensor : Binary Sensor (routing) (04 : 20 : 01)
Z-Wave Debug . . getNodeProtocolInfo: base class command: 30
Z-Wave Debug . . getNodeProtocolInfo: supported class commands: 20 30
Z-Wave Debug . . getNodeProtocolInfo: features: routing, beaming
Z-Wave found module included in controller with no matching device (missing or disabled): 002 - Binary Sensor (routing)
Z-Wave Debug SENT getNodeProtocolInfo: 01 04 00 41 03 B9
Z-Wave Debug RCVD getNodeProtocolInfo: 01 09 01 41 53 9C 00 04 08 04 71
Z-Wave Debug . . getNodeProtocolInfo: node 003, class name: Setpoint Thermostat
Z-Wave Debug . . getNodeProtocolInfo: class hierarchy: Routing Slave : Thermostat : Setpoint Thermostat (04 : 08 : 04)
Z-Wave Debug . . getNodeProtocolInfo: base class command: 43
Z-Wave Debug . . getNodeProtocolInfo: supported class commands: 20 72 43 86 8F
Z-Wave Debug . . getNodeProtocolInfo: features: routing, beaming
Z-Wave found module included in controller with no matching device (missing or disabled): 003 - Setpoint Thermostat
Z-Wave Debug SENT getNodeProtocolInfo: 01 04 00 41 04 BE
Z-Wave Debug RCVD getNodeProtocolInfo: 01 09 01 41 53 9C 00 04 20 01 5C
Z-Wave Debug . . getNodeProtocolInfo: node 004, class name: Binary Sensor (routing)
Z-Wave Debug . . getNodeProtocolInfo: class hierarchy: Routing Slave : Binary Sensor : Binary Sensor (routing) (04 : 20 : 01)
Z-Wave Debug . . getNodeProtocolInfo: base class command: 30
Z-Wave Debug . . getNodeProtocolInfo: supported class commands: 20 30
Z-Wave Debug . . getNodeProtocolInfo: features: routing, beaming
Z-Wave found module included in controller with no matching device (missing or disabled): 004 - Binary Sensor (routing)
Z-Wave Debug SENT getNodeProtocolInfo: 01 04 00 41 05 BF
Z-Wave Debug RCVD getNodeProtocolInfo: 01 09 01 41 D3 9C 00 04 0F 01 F3
Z-Wave Debug . . getNodeProtocolInfo: node 005, class name: Basic Repeater Slave
Z-Wave Debug . . getNodeProtocolInfo: class hierarchy: Routing Slave : Repeater Slave : Basic Repeater Slave (04 : 0F : 01)
Z-Wave Debug . . getNodeProtocolInfo: base class command: 00
Z-Wave Debug . . getNodeProtocolInfo: supported class commands: 20
Z-Wave Debug . . getNodeProtocolInfo: features: routing, beaming
Z-Wave found module included in controller with no matching device (missing or disabled): 005 - Basic Repeater Slave
Z-Wave Debug starting node status polling
*/

namespace ZWave
{
    class Program
    {
        static void Main(string[] args)
        {
            byte nodeId = 0x06;

            byte level = 0xFF; // On
            byte[] message = new byte[] { 0x01, 0x09, 0x00, 0x13, nodeId, 0x03, 0x20, 0x01, level, 0x05, 0x00 };

            ZWavePort zp = new ZWavePort();
            zp.Open();

            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            //
            System.Console.WriteLine();
            level = 0x00; // Off
            message = new byte[] { 0x01, 0x09, 0x00, 0x13, nodeId, 0x03, 0x20, 0x01, level, 0x05, 0x00 };
            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            /*
            //disable disco
            message = new byte[] { 0x01, 0x08, 0x00, 0xF2, 0x51, 0x01, 0x00, 0x05, 0x01, 0x51, 0x00 };
            //enable disco
            message = new byte[] { 0x01, 0x08, 0x00, 0xF2, 0x51, 0x01, 0x01, 0x05, 0x01, 0x50, 0x00 };
            // Test
            //message = new byte[] { 0x01, 0x25, 0x01, 0x02, 0x05, 0x08, 0x1d, 0x79, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            */
            // getHomeID
            message = new byte[] { 0x01,0x03,0x00,0x20,0x0 };
            System.Console.WriteLine();
            System.Console.WriteLine("Get home ID");
            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            //
            // get Version
            message = new byte[] { 0x01, 0x03, 0x00, 0x15, 0x0 };
            System.Console.WriteLine();
            System.Console.WriteLine("Get version");
            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            //
            // get Nodes
            message = new byte[] { 0x01, 0x03, 0x00, 0x2, 0x0 };
            System.Console.WriteLine();
            System.Console.WriteLine("Get nodes");
            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            /*
            // get Interface Features
            message = new byte[] { 0x01, 0x03, 0x00, 0x05, 0x0 };
            System.Console.WriteLine();
            System.Console.WriteLine("Get Interface Features");
            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            //
            // get Interface Api Capabilities
            message = new byte[] { 0x01, 0x03, 0x00, 0x07, 0x0 };
            System.Console.WriteLine();
            System.Console.WriteLine("Get Interface Api Capabilities");
            zp.SendMessage(message);
            Thread.Sleep(5000); // Wait for 5 seconds
            */
            System.Console.ReadLine(); // Wait for the user to terminate the program

            zp.Close();
        }
    }
}
