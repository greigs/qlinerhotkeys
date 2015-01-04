using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting.Channels.Tcp;

namespace HotKeys
{

	public delegate string MyEventHandler(object sender, MyEventArgs mea);

	[ Serializable ]
	public class MyEventArgs: EventArgs 
	{
		public MyEventArgs(string []strNewArgs, bool NewInst) 
		{
			this.strArgs = strNewArgs;
			this.bNewInstance = NewInst;
		}

		public string []strArgs;
		public bool	bNewInstance;
	}


	public class SingleInstanceHandler : MarshalByRefObject
	{
		private System.Threading.Mutex mutex;
		private string uniqueIdentifier;
		private TcpChannel tcpChannel;

		public event MyEventHandler MyEvent;

		public void Run(string []strArgs)
		{
			uniqueIdentifier = "HotKeyApp::" + Environment.UserName + "@" + Environment.UserDomainName;
			mutex = new System.Threading.Mutex(false, uniqueIdentifier);
			if(mutex.WaitOne(1,true))
			{
				CreateInstanceChannel();
				MyEventArgs EvArg = new MyEventArgs(strArgs,true);
				Console.WriteLine( RaiseStartUpEvent(EvArg) );
			}
			else
			{
				if(strArgs.Length == 0)
					strArgs = new string[1]{"Instructions"};
				MyEventArgs EvArg = new MyEventArgs(strArgs,false);
				Console.WriteLine( UseInstanceChannel(EvArg) );
			}
		}

		private void CreateInstanceChannel()
		{
			System.Runtime.Remoting.RemotingServices.Marshal(this, uniqueIdentifier);
			IDictionary tcpProperties = new Hashtable(2);
			tcpProperties.Add("bindTo","127.0.0.1");
			tcpProperties.Add("port",0);
			tcpChannel = new TcpChannel(tcpProperties,null,null);
			System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(tcpChannel);
			Microsoft.Win32.RegistryKey key = Application.UserAppDataRegistry;
			key.SetValue(uniqueIdentifier, tcpChannel.GetUrlsForUri(uniqueIdentifier));
		}

		private string UseInstanceChannel(MyEventArgs event_args)
		{
			Microsoft.Win32.RegistryKey key = Application.UserAppDataRegistry;
			string []strArray = (string[])key.GetValue(uniqueIdentifier);
			Type typ = typeof(SingleInstanceHandler);
			object obj1 = System.Runtime.Remoting.RemotingServices.Connect(typ,strArray[0]);

			SingleInstanceHandler remote_component = (SingleInstanceHandler)obj1;
			return remote_component.RaiseStartUpEvent(event_args);
		}

		public string RaiseStartUpEvent(MyEventArgs event_args)
		{
			return MyEvent(this, event_args);
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}


	}

//	public delegate void SingleInstanceEventHandler(object sender, SingleInstanceEventArgs singleInstanceEventArgs);
//
//	[Serializable]
//	public class SingleInstanceEventArgs: EventArgs 
//	{		
//		public string[] Args;
//		public bool	NewInstance;
//
//		public SingleInstanceEventArgs(string[] args, bool newInstance) 
//		{
//			this.Args = args;
//			this.NewInstance = newInstance;
//		}
//	}
//
//
//	public class SingleInstanceApplication : MarshalByRefObject
//	{
//		private System.Threading.Mutex mutex;
//		private string uniqueIdentifier;
//		private TcpChannel tcpChannel;
//
//		public event SingleInstanceEventHandler InstanceEvent;
//
//		public void Run(string[] args)
//		{
//			uniqueIdentifier = "HotKeyApp::" + Environment.UserName + "@" + Environment.UserDomainName;
//			mutex = new System.Threading.Mutex(false, uniqueIdentifier);
//			if(mutex.WaitOne(1,true))
//			{
//				CreateInstanceChannel();
//				SingleInstanceEventArgs singleInstanceEventArgs = new SingleInstanceEventArgs(args,true);
//				RaiseStartUpEvent(singleInstanceEventArgs);
//			}
//			else
//			{
//				SingleInstanceEventArgs singleInstanceEventArgs = new SingleInstanceEventArgs(args,false);
//				UseInstanceChannel(singleInstanceEventArgs);
//			}
//		}
//
//		private void CreateInstanceChannel()
//		{
//			System.Runtime.Remoting.RemotingServices.Marshal(this, uniqueIdentifier);
//			IDictionary tcpProperties = new Hashtable(2);
//			tcpProperties.Add("bindTo","127.0.0.1");
//			tcpProperties.Add("port",0);
//			tcpChannel = new TcpChannel(tcpProperties,null,null);
//			System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(tcpChannel);
//			Microsoft.Win32.RegistryKey key = Application.UserAppDataRegistry;
//			key.SetValue(uniqueIdentifier, tcpChannel.GetUrlsForUri(uniqueIdentifier));
//		}
//
//		private void UseInstanceChannel(SingleInstanceEventArgs singleInstanceEventArgs)
//		{
//			Microsoft.Win32.RegistryKey key = Application.UserAppDataRegistry;
//			string[] registryKeyValue = (string[])key.GetValue(uniqueIdentifier);
//			Type remoteType = typeof(SingleInstanceApplication);
//			object o = System.Runtime.Remoting.RemotingServices.Connect(remoteType,registryKeyValue[0]);
//			SingleInstanceApplication remoteComponent = (SingleInstanceApplication)o;
//			remoteComponent.RaiseStartUpEvent(singleInstanceEventArgs);
//		}
//
//		public void RaiseStartUpEvent(SingleInstanceEventArgs singleInstanceEventArgs)
//		{
//			InstanceEvent(this, singleInstanceEventArgs);
//		}

	}
