/*
				   File: type_SdtdeleteMesage__postInput
			Description: deleteMesage__postInput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="deleteMesage__postInput")]
	[XmlType(TypeName="deleteMesage__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdeleteMesage__postInput : GxUserType
	{
		public SdtdeleteMesage__postInput( )
		{
			/* Constructor for serialization */
		}

		public SdtdeleteMesage__postInput(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("MessageId", gxTpr_Messageid, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="MessageId")]
		[XmlElement(ElementName="MessageId")]
		public Guid gxTpr_Messageid
		{
			get {
				return gxTv_SdtdeleteMesage__postInput_Messageid; 
			}
			set {
				gxTv_SdtdeleteMesage__postInput_Messageid = value;
				SetDirty("Messageid");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtdeleteMesage__postInput_Messageid;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deleteMesage__postInput", Namespace="distributedcryptography")]
	public class SdtdeleteMesage__postInput_RESTInterface : GxGenericCollectionItem<SdtdeleteMesage__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdeleteMesage__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtdeleteMesage__postInput_RESTInterface( SdtdeleteMesage__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="MessageId", Order=0)]
		public Guid gxTpr_Messageid
		{
			get { 
				return sdt.gxTpr_Messageid;

			}
			set { 
				sdt.gxTpr_Messageid = value;
			}
		}


		#endregion

		public SdtdeleteMesage__postInput sdt
		{
			get { 
				return (SdtdeleteMesage__postInput)Sdt;
			}
			set { 
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtdeleteMesage__postInput() ;
			}
		}
	}
	#endregion
}