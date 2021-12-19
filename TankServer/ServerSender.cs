﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TankServer
{
    internal class ServerSender
    {
        //sError
        public static void ErrorSender(int _clientId, string _message)
        {
            Packet packet = new Packet(0x01, (byte)ServerPackets.sError);
            packet.Write(_message);
            Server.clients[_clientId].tcp.SendData(packet);

        }

        //sConnect
        public static void ConnectSender(int _clientId)
        {
            Packet packet = new Packet(0x01, (byte)ServerPackets.sConnect);
            packet.Write(_clientId);
            Server.clients[_clientId].tcp.SendData(packet);
        }

        //sRoomInfo
        public static void RoomInfoSender(int _clientId, int _roomId, bool _toAll = false)
        {
            Packet packet = new Packet(0x01, (byte)ServerPackets.sRoomInfo);
            packet.Write(_roomId);
            packet.Write(Server.rooms[_roomId].roomName);
            packet.Write(Server.rooms[_roomId].clients.Count);
            for (int i = 0; i < Server.rooms[_roomId].clients.Count; i++)
            {
                packet.Write(Server.rooms[_roomId].clients[i].id);
                packet.Write(Server.rooms[_roomId].clients[i].username);
                packet.Write(Server.rooms[_roomId].clients[i].teamId);
                packet.Write(Server.rooms[_roomId].clients[i].tankId);
            }
            if (_toAll)
            {
                foreach (Client _client in Server.rooms[_roomId].clients)
                {
                    _client.tcp.SendData(packet);
                }
            }
            else
            {
                Server.clients[_clientId].tcp.SendData(packet);
            }
        }

        //sRoomList
        public static void RoomListSender(int _clientId)
        {
            Packet packet = new Packet(0x01, (byte)ServerPackets.sRoomList);
            int count = 0;
            for (int i = 0; i <= Server.rooms.Count; i++)
            {
                if (!Server.rooms[i].isInGame)
                {
                    count++;
                }
            }
            packet.Write(count);
            for (int i = 0; i <= Server.rooms.Count; i++)
            {
                if (!Server.rooms[i].isInGame)
                {
                    packet.Write(Server.rooms[i].roomId);
                    packet.Write(Server.rooms[i].roomName);
                    packet.Write(Server.rooms[i].clients.Count);
                }
            }
            Server.clients[_clientId].tcp.SendData(packet);
        }

        //sGameStart
        public static void GameStartSender(int _roomId)
        {
            Packet packet = new Packet(0x01, (byte)ServerPackets.sGameStart);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sTankPosition
        public static void TankPositionSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sTankPosition);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sTankShoot
        public static void TankShootSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sTankShoot);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sTankSpecial
        public static void TankSpecialSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sTankSpecial);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sTankHealth
        public static void TankHealthSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sTankHealth);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sTankDeath
        public static void TankDeathSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sTankDeath);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sWinRound
        public static void WinRoundSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sWinRound);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sWinGame
        public static void WinGameSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sWinGame);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

        //sDisconnect
        public static void DisconnectSender(Packet packet, int _roomId)
        {
            packet.OverwriteHeader(0x01, (byte)ServerPackets.sDisconnect);
            foreach (Client _client in Server.rooms[_roomId].clients)
            {
                _client.tcp.SendData(packet);
            }
        }

    }
}