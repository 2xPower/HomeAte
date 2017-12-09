﻿using TwicePower.Homeate.Binding.Knx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twicepower.Homeate.Contracts;

namespace TwicePower.Homeate.Bdingin.Knx
{
    public class KnxBinding : Twicepower.Homeate.Contracts.Binding
    {
        TwicePower.Homeate.Binding.Knx.KnxConnection _knxConnection;
        private TaskCompletionSource<bool> _tcs;

       

        #region Knx Connection event handlers
        private void Status(string address, string state)
        {
            
        }

        private void Event(string address, string state)
        {
            
        }

        private void Disconnected()
        {
            _tcs.SetResult(false);
        }

        private void Connected()
        {
            _tcs.SetResult(false);
        }

        protected override void InitCore()
        {
            var connection = new KnxConnectionTunneling("10.0.0.31", 3671, "10.0.0.24", 3671) { Debug = false };
            connection.KnxConnectedDelegate += Connected;
            connection.KnxDisconnectedDelegate += Disconnected;
            connection.KnxEventDelegate += Event;
            connection.KnxStatusDelegate += Status;
            this._knxConnection = connection;
        }

        protected override void ConnectCore()
        {
            _tcs = new TaskCompletionSource<bool>();
            this._knxConnection.Connect();
            _tcs.Task.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected override void DisconnectCore()
        {
            _tcs = new TaskCompletionSource<bool>();
            this._knxConnection.Disconnect();
            _tcs.Task.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        #endregion

    }
}