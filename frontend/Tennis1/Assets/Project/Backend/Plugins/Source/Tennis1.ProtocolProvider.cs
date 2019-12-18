
            using System;  
            using System.Collections.Generic;
            
            namespace Tennis1{ 
                public class ProtocolProvider : Regulus.Remote.IProtocol
                {
                    Regulus.Remote.InterfaceProvider _InterfaceProvider;
                    Regulus.Remote.EventProvider _EventProvider;
                    Regulus.Remote.MemberMap _MemberMap;
                    Regulus.Serialization.ISerializer _Serializer;
                    public ProtocolProvider()
                    {
                        var types = new Dictionary<Type, Type>();
                        types.Add(typeof(Tennis1.Common.IControll) , typeof(Tennis1.Common.Ghost.CIControll) );
types.Add(typeof(Tennis1.Common.IMatchable) , typeof(Tennis1.Common.Ghost.CIMatchable) );
types.Add(typeof(Tennis1.Common.IPlayer) , typeof(Tennis1.Common.Ghost.CIPlayer) );
types.Add(typeof(Tennis1.Common.IPlayground) , typeof(Tennis1.Common.Ghost.CIPlayground) );
types.Add(typeof(Tennis1.Common.IPreparable) , typeof(Tennis1.Common.Ghost.CIPreparable) );
types.Add(typeof(Tennis1.Common.IPreparer) , typeof(Tennis1.Common.Ghost.CIPreparer) );
                        _InterfaceProvider = new Regulus.Remote.InterfaceProvider(types);

                        var eventClosures = new List<Regulus.Remote.IEventProxyCreator>();
                        eventClosures.Add(new Tennis1.Common.Invoker.IPlayer.MoveEvent() );
                        _EventProvider = new Regulus.Remote.EventProvider(eventClosures);

                        _Serializer = new Regulus.Serialization.Serializer(new Regulus.Serialization.DescriberBuilder(typeof(Regulus.CustomType.Point),typeof(Regulus.CustomType.Vector2),typeof(Regulus.Remote.ClientToServerOpCode),typeof(Regulus.Remote.PackageCallMethod),typeof(Regulus.Remote.PackageErrorMethod),typeof(Regulus.Remote.PackageInvokeEvent),typeof(Regulus.Remote.PackageLoadSoul),typeof(Regulus.Remote.PackageLoadSoulCompile),typeof(Regulus.Remote.PackageProtocolSubmit),typeof(Regulus.Remote.PackageRelease),typeof(Regulus.Remote.PackageReturnValue),typeof(Regulus.Remote.PackageUnloadSoul),typeof(Regulus.Remote.PackageUpdateProperty),typeof(Regulus.Remote.RequestPackage),typeof(Regulus.Remote.ResponsePackage),typeof(Regulus.Remote.ServerToClientOpCode),typeof(System.Boolean),typeof(System.Byte[]),typeof(System.Byte[][]),typeof(System.Char),typeof(System.Char[]),typeof(System.Double),typeof(System.Guid),typeof(System.Int32),typeof(System.Single),typeof(System.String),typeof(Tennis1.Common.Move),typeof(Tennis1.Common.Registration)).Describers);


                        _MemberMap = new Regulus.Remote.MemberMap(new System.Reflection.MethodInfo[] {new Regulus.Remote.AOT.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Tennis1.Common.IControll,Regulus.CustomType.Vector2>>)((ins,_1) => ins.Move(_1))).Method,new Regulus.Remote.AOT.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Tennis1.Common.IMatchable>>)((ins) => ins.Cancel())).Method,new Regulus.Remote.AOT.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Tennis1.Common.IPlayground>>)((ins) => ins.Exit())).Method,new Regulus.Remote.AOT.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Tennis1.Common.IPreparable>>)((ins) => ins.Ready())).Method,new Regulus.Remote.AOT.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Tennis1.Common.IPreparer,Tennis1.Common.Registration>>)((ins,_1) => ins.SignUp(_1))).Method} ,new System.Reflection.EventInfo[]{ typeof(Tennis1.Common.IPlayer).GetEvent("MoveEvent") }, new System.Reflection.PropertyInfo[] { }, new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>[] {new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Tennis1.Common.IControll),()=>new Regulus.Remote.TProvider<Tennis1.Common.IControll>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Tennis1.Common.IMatchable),()=>new Regulus.Remote.TProvider<Tennis1.Common.IMatchable>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Tennis1.Common.IPlayer),()=>new Regulus.Remote.TProvider<Tennis1.Common.IPlayer>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Tennis1.Common.IPlayground),()=>new Regulus.Remote.TProvider<Tennis1.Common.IPlayground>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Tennis1.Common.IPreparable),()=>new Regulus.Remote.TProvider<Tennis1.Common.IPreparable>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Tennis1.Common.IPreparer),()=>new Regulus.Remote.TProvider<Tennis1.Common.IPreparer>())});
                    }

                    byte[] Regulus.Remote.IProtocol.VerificationCode { get { return new byte[]{168,76,199,138,156,228,86,54,71,199,118,248,74,11,218,9};} }
                    Regulus.Remote.InterfaceProvider Regulus.Remote.IProtocol.GetInterfaceProvider()
                    {
                        return _InterfaceProvider;
                    }

                    Regulus.Remote.EventProvider Regulus.Remote.IProtocol.GetEventProvider()
                    {
                        return _EventProvider;
                    }

                    Regulus.Serialization.ISerializer Regulus.Remote.IProtocol.GetSerialize()
                    {
                        return _Serializer;
                    }

                    Regulus.Remote.MemberMap Regulus.Remote.IProtocol.GetMemberMap()
                    {
                        return _MemberMap;
                    }
                    
                }
            }
            