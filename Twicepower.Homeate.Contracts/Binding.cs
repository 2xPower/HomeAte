using System;
using System.Threading.Tasks;

namespace Twicepower.Homeate.Contracts
{
    public abstract class Binding
    {
        private BindingState _state;

        public event Action<Binding, BindingState> BindingStateChanged;

        public BindingState State
        {
            get => _state;
            set
            {
                _state = value;
                BindingStateChanged?.Invoke(this, _state);
            }
        }

        public async Task Init()
        {
            if (this.State == BindingState.Uninitialized)
            {
                try
                {
                    this.State = BindingState.Initializing;
                    await Task.Factory.StartNew(() => this.InitCore());
                    this.State = BindingState.Disconnected;
                }
                catch (Exception ex)
                {
                    throw ex; 
                }
            }
            else throw new Exception("the binding is already initialized.");
        }

        protected abstract void InitCore();

        public async Task Connect()
        {
            if (this.State == BindingState.Disconnected)
            {
                try
                {
                    this.State = BindingState.Connecting;
                    await Task.Factory.StartNew(() => this.ConnectCore());
                    this.State = BindingState.Connected;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else throw new Exception("the binding is net yet initialized (expected state is disconnected).");
        }

        protected abstract Task ConnectCore();

        public async Task Disconnect()
        {
            if (this.State == BindingState.Connected)
            {
                try
                {
                    this.State = BindingState.Disconnecting;
                    await Task.Factory.StartNew(() => this.DisconnectCore());
                    this.State = BindingState.Disconnected;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else throw new Exception("the binding is not connected.");
        }

        protected abstract Task DisconnectCore();
    }
}
