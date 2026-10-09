using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet.registered {
   public class wwcontacts : GXDataArea
   {
      public wwcontacts( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public wwcontacts( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId ,
                           Guid aP1_passwordId )
      {
         this.AV17groupId = aP0_groupId;
         this.AV59passwordId = aP1_passwordId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavUsername = new GXCombobox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "groupId");
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "groupId");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "groupId");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridusers") == 0 )
            {
               gxnrGridusers_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridusers") == 0 )
            {
               gxgrGridusers_refresh_invoke( ) ;
               return  ;
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               AV17groupId = StringUtil.StrToGuid( gxfirstwebparm);
               AssignAttri("", false, "AV17groupId", AV17groupId.ToString());
               GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV17groupId, context));
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
               {
                  AV59passwordId = StringUtil.StrToGuid( GetPar( "passwordId"));
                  AssignAttri("", false, "AV59passwordId", AV59passwordId.ToString());
                  GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORDID", GetSecureSignedToken( "", AV59passwordId, context));
               }
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGridusers_newrow_invoke( )
      {
         nRC_GXsfl_11 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_11"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_11_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_11_idx = GetPar( "sGXsfl_11_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridusers_newrow( ) ;
         /* End function gxnrGridusers_newrow_invoke */
      }

      protected void gxgrGridusers_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV57availableContacts);
         AV17groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         AV59passwordId = StringUtil.StrToGuid( GetPar( "passwordId"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridusers_refresh( AV57availableContacts, AV17groupId, AV59passwordId) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridusers_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA312( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START312( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.wwcontacts", new object[] {UrlEncode(AV17groupId.ToString()),UrlEncode(AV59passwordId.ToString())}, new string[] {"groupId","passwordId"}) +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAVAILABLECONTACTS", AV57availableContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAVAILABLECONTACTS", AV57availableContacts);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vAVAILABLECONTACTS", GetSecureSignedToken( "", AV57availableContacts, context));
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV17groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV17groupId, context));
         GxWebStd.gx_hidden_field( context, "vPASSWORDID", AV59passwordId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORDID", GetSecureSignedToken( "", AV59passwordId, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Contacts", AV42contacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Contacts", AV42contacts);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCONTACTS", AV42contacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCONTACTS", AV42contacts);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAVAILABLECONTACTS", AV57availableContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAVAILABLECONTACTS", AV57availableContacts);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vAVAILABLECONTACTS", GetSecureSignedToken( "", AV57availableContacts, context));
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV17groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV17groupId, context));
         GxWebStd.gx_hidden_field( context, "vPASSWORDID", AV59passwordId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORDID", GetSecureSignedToken( "", AV59passwordId, context));
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE312( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT312( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.registered.wwcontacts", new object[] {UrlEncode(AV17groupId.ToString()),UrlEncode(AV59passwordId.ToString())}, new string[] {"groupId","passwordId"})  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.WWContacts" ;
      }

      public override string GetPgmdesc( )
      {
         return "WWContacts" ;
      }

      protected void WB310( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUsername_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'" + sGXsfl_11_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUsername, cmbavUsername_Internalname, AV36userName.ToString(), 1, cmbavUsername_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "guid", "", 1, cmbavUsername.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", "", true, 0, "HLP_Wallet/registered/WWContacts.htm");
            cmbavUsername.CurrentValue = AV36userName.ToString();
            AssignProp("", false, cmbavUsername_Internalname, "Values", (string)(cmbavUsername.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridusersContainer.SetWrapped(nGXWrapped);
            StartGridControl11( ) ;
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            nRC_GXsfl_11 = (int)(nGXsfl_11_idx-1);
            if ( GridusersContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV60GXV1 = nGXsfl_11_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridusersContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridusers", GridusersContainer, subGridusers_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridusersContainerData", GridusersContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridusersContainerData"+"V", GridusersContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridusersContainerData"+"V"+"\" value='"+GridusersContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/WWContacts.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/WWContacts.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridusersContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV60GXV1 = nGXsfl_11_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridusersContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridusers", GridusersContainer, subGridusers_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridusersContainerData", GridusersContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridusersContainerData"+"V", GridusersContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridusersContainerData"+"V"+"\" value='"+GridusersContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START312( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
            }
         }
         Form.Meta.addItem("description", "WWContacts", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP310( ) ;
      }

      protected void WS312( )
      {
         START312( ) ;
         EVT312( ) ;
      }

      protected void EVT312( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VUSERNAME.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E11312 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Save' */
                              E12312 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E13312 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDUSERS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'REMOVE USER'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'REMOVE USER'") == 0 ) )
                           {
                              nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
                              SubsflControlProps_112( ) ;
                              AV60GXV1 = nGXsfl_11_idx;
                              if ( ( AV42contacts.Count >= AV60GXV1 ) && ( AV60GXV1 > 0 ) )
                              {
                                 AV42contacts.CurrentItem = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1));
                                 AV10deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV10deleteImage)) ? AV66Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV10deleteImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV10deleteImage), true);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E14312 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDUSERS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridusers.Load */
                                    E15312 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'REMOVE USER'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Remove User' */
                                    E16312 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE312( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA312( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = cmbavUsername_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridusers_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_112( ) ;
         while ( nGXsfl_11_idx <= nRC_GXsfl_11 )
         {
            sendrow_112( ) ;
            nGXsfl_11_idx = ((subGridusers_Islastpage==1)&&(nGXsfl_11_idx+1>subGridusers_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridusersContainer)) ;
         /* End function gxnrGridusers_newrow */
      }

      protected void gxgrGridusers_refresh( GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV57availableContacts ,
                                            Guid AV17groupId ,
                                            Guid AV59passwordId )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDUSERS_nCurrentRecord = 0;
         RF312( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridusers_refresh */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
         if ( cmbavUsername.ItemCount > 0 )
         {
            AV36userName = StringUtil.StrToGuid( cmbavUsername.getValidValue(AV36userName.ToString()));
            AssignAttri("", false, "AV36userName", AV36userName.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUsername.CurrentValue = AV36userName.ToString();
            AssignProp("", false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF312( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlcontactid_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
      }

      protected void RF312( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridusersContainer.ClearRows();
         }
         wbStart = 11;
         nGXsfl_11_idx = 1;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         bGXsfl_11_Refreshing = true;
         GridusersContainer.AddObjectProperty("GridName", "Gridusers");
         GridusersContainer.AddObjectProperty("CmpContext", "");
         GridusersContainer.AddObjectProperty("InMasterPage", "false");
         GridusersContainer.AddObjectProperty("Class", "Grid");
         GridusersContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridusersContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridusersContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Backcolorstyle), 1, 0, ".", "")));
         GridusersContainer.PageSize = subGridusers_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_112( ) ;
            /* Execute user event: Gridusers.Load */
            E15312 ();
            wbEnd = 11;
            WB310( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes312( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAVAILABLECONTACTS", AV57availableContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAVAILABLECONTACTS", AV57availableContacts);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vAVAILABLECONTACTS", GetSecureSignedToken( "", AV57availableContacts, context));
      }

      protected int subGridusers_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridusers_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridusers_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridusers_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlcontactid_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP310( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14312 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Contacts"), AV42contacts);
            ajax_req_read_hidden_sdt(cgiGet( "vCONTACTS"), AV42contacts);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subGridusers_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subGridusers_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV60GXV1 = nGXsfl_11_fel_idx;
               if ( ( AV42contacts.Count >= AV60GXV1 ) && ( AV60GXV1 > 0 ) )
               {
                  AV42contacts.CurrentItem = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1));
                  AV10deleteImage = cgiGet( edtavDeleteimage_Internalname);
               }
            }
            if ( nGXsfl_11_fel_idx == 0 )
            {
               nGXsfl_11_idx = 1;
               sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
               SubsflControlProps_112( ) ;
            }
            nGXsfl_11_fel_idx = 1;
            /* Read variables values. */
            cmbavUsername.Name = cmbavUsername_Internalname;
            cmbavUsername.CurrentValue = cgiGet( cmbavUsername_Internalname);
            AV36userName = StringUtil.StrToGuid( cgiGet( cmbavUsername_Internalname));
            AssignAttri("", false, "AV36userName", AV36userName.ToString());
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E14312 ();
         if (returnInSub) return;
      }

      protected void E14312( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_char1 = AV13error;
         new GeneXus.Programs.wallet.getvaultpasswordlinks(context ).execute(  AV17groupId,  AV59passwordId, out  AV58description, out  AV56assignedTags, out  AV55allTags, out  AV42contacts, out  AV57availableContacts, out  GXt_char1) ;
         gx_BV11 = true;
         AV13error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GX_msglist.addItem(AV13error);
         }
         cmbavUsername.removeAllItems();
         cmbavUsername.addItem(StringUtil.StrToGuid( "").ToString(), "Select a Contact to Add", 0);
         AV63GXV4 = 1;
         while ( AV63GXV4 <= AV57availableContacts.Count )
         {
            AV19oneContact = ((GeneXus.Programs.wallet.SdtVaultContact)AV57availableContacts.Item(AV63GXV4));
            cmbavUsername.addItem(AV19oneContact.gxTpr_Contactid.ToString(), AV19oneContact.gxTpr_Contactprivatename, 0);
            AV63GXV4 = (int)(AV63GXV4+1);
         }
         edtavCtlcontactprivatename_Title = "Assigned to "+StringUtil.Trim( AV58description);
         AssignProp("", false, edtavCtlcontactprivatename_Internalname, "Title", edtavCtlcontactprivatename_Title, !bGXsfl_11_Refreshing);
      }

      protected void E11312( )
      {
         AV60GXV1 = nGXsfl_11_idx;
         if ( ( AV60GXV1 > 0 ) && ( AV42contacts.Count >= AV60GXV1 ) )
         {
            AV42contacts.CurrentItem = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1));
         }
         /* Username_Controlvaluechanged Routine */
         returnInSub = false;
         AV45found = false;
         AV64GXV5 = 1;
         while ( AV64GXV5 <= AV42contacts.Count )
         {
            AV19oneContact = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV64GXV5));
            if ( AV19oneContact.gxTpr_Contactid == AV36userName )
            {
               AV45found = true;
            }
            AV64GXV5 = (int)(AV64GXV5+1);
         }
         if ( ! AV45found && ! (Guid.Empty==AV36userName) )
         {
            AV65GXV6 = 1;
            while ( AV65GXV6 <= AV57availableContacts.Count )
            {
               AV19oneContact = ((GeneXus.Programs.wallet.SdtVaultContact)AV57availableContacts.Item(AV65GXV6));
               if ( AV19oneContact.gxTpr_Contactid == AV36userName )
               {
                  AV42contacts.Add((GeneXus.Programs.wallet.SdtVaultContact)(AV19oneContact.Clone()), 0);
                  gx_BV11 = true;
               }
               AV65GXV6 = (int)(AV65GXV6+1);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV42contacts", AV42contacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridusers_refresh( AV57availableContacts, AV17groupId, AV59passwordId) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void E12312( )
      {
         AV60GXV1 = nGXsfl_11_idx;
         if ( ( AV60GXV1 > 0 ) && ( AV42contacts.Count >= AV60GXV1 ) )
         {
            AV42contacts.CurrentItem = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1));
         }
         /* 'Save' Routine */
         returnInSub = false;
         GXt_char1 = AV13error;
         new GeneXus.Programs.wallet.setvaultpasswordcontacts(context ).execute(  AV17groupId,  AV59passwordId,  AV42contacts, out  GXt_char1) ;
         AV13error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         GX_msglist.addItem(AV13error);
      }

      protected void E13312( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      private void E15312( )
      {
         /* Gridusers_Load Routine */
         returnInSub = false;
         AV60GXV1 = 1;
         while ( AV60GXV1 <= AV42contacts.Count )
         {
            AV42contacts.CurrentItem = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV10deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri("", false, edtavDeleteimage_Internalname, AV10deleteImage);
            AV66Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 11;
            }
            sendrow_112( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_11_Refreshing )
            {
               DoAjaxLoad(11, GridusersRow);
            }
            AV60GXV1 = (int)(AV60GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E16312( )
      {
         AV60GXV1 = nGXsfl_11_idx;
         if ( ( AV60GXV1 > 0 ) && ( AV42contacts.Count >= AV60GXV1 ) )
         {
            AV42contacts.CurrentItem = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1));
         }
         /* 'Remove User' Routine */
         returnInSub = false;
         AV67GXV7 = 1;
         while ( AV67GXV7 <= AV42contacts.Count )
         {
            AV19oneContact = ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV67GXV7));
            if ( AV19oneContact.gxTpr_Contactid == ((GeneXus.Programs.wallet.SdtVaultContact)(AV42contacts.CurrentItem)).gxTpr_Contactid )
            {
               AV42contacts.RemoveItem(AV42contacts.IndexOf(AV19oneContact));
               gx_BV11 = true;
            }
            AV67GXV7 = (int)(AV67GXV7+1);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV42contacts", AV42contacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridusers_refresh( AV57availableContacts, AV17groupId, AV59passwordId) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV17groupId = (Guid)getParm(obj,0);
         AssignAttri("", false, "AV17groupId", AV17groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV17groupId, context));
         AV59passwordId = (Guid)getParm(obj,1);
         AssignAttri("", false, "AV59passwordId", AV59passwordId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPASSWORDID", GetSecureSignedToken( "", AV59passwordId, context));
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA312( ) ;
         WS312( ) ;
         WE312( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20261071417698", true, true, false);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         context.AddJavascriptSource("wallet/registered/wwcontacts.js", "?20261071417698", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_112( )
      {
         edtavCtlcontactid_Internalname = "CTLCONTACTID_"+sGXsfl_11_idx;
         edtavCtlcontactprivatename_Internalname = "CTLCONTACTPRIVATENAME_"+sGXsfl_11_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_idx;
      }

      protected void SubsflControlProps_fel_112( )
      {
         edtavCtlcontactid_Internalname = "CTLCONTACTID_"+sGXsfl_11_fel_idx;
         edtavCtlcontactprivatename_Internalname = "CTLCONTACTPRIVATENAME_"+sGXsfl_11_fel_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_fel_idx;
      }

      protected void sendrow_112( )
      {
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         WB310( ) ;
         GridusersRow = GXWebRow.GetNew(context,GridusersContainer);
         if ( subGridusers_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridusers_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridusers_Class, "") != 0 )
            {
               subGridusers_Linesclass = subGridusers_Class+"Odd";
            }
         }
         else if ( subGridusers_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridusers_Backstyle = 0;
            subGridusers_Backcolor = subGridusers_Allbackcolor;
            if ( StringUtil.StrCmp(subGridusers_Class, "") != 0 )
            {
               subGridusers_Linesclass = subGridusers_Class+"Uniform";
            }
         }
         else if ( subGridusers_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridusers_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridusers_Class, "") != 0 )
            {
               subGridusers_Linesclass = subGridusers_Class+"Odd";
            }
            subGridusers_Backcolor = (int)(0x0);
         }
         else if ( subGridusers_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridusers_Backstyle = 1;
            if ( ((int)((nGXsfl_11_idx) % (2))) == 0 )
            {
               subGridusers_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridusers_Class, "") != 0 )
               {
                  subGridusers_Linesclass = subGridusers_Class+"Even";
               }
            }
            else
            {
               subGridusers_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridusers_Class, "") != 0 )
               {
                  subGridusers_Linesclass = subGridusers_Class+"Odd";
               }
            }
         }
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_11_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridusersRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactid_Internalname,((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1)).gxTpr_Contactid.ToString(),((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1)).gxTpr_Contactid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlcontactid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)11,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridusersRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtVaultContact)AV42contacts.Item(AV60GXV1)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV10deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV10deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV66Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV10deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV10deleteImage)) ? AV66Deleteimage_GXI : context.PathToRelativeUrl( AV10deleteImage));
         GridusersRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'REMOVE USER\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV10deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes312( ) ;
         GridusersContainer.AddRow(GridusersRow);
         nGXsfl_11_idx = ((subGridusers_Islastpage==1)&&(nGXsfl_11_idx+1>subGridusers_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         /* End function sendrow_112 */
      }

      protected void init_web_controls( )
      {
         cmbavUsername.Name = "vUSERNAME";
         cmbavUsername.WebTags = "";
         if ( cmbavUsername.ItemCount > 0 )
         {
            AV36userName = StringUtil.StrToGuid( cmbavUsername.getValidValue(AV36userName.ToString()));
            AssignAttri("", false, "AV36userName", AV36userName.ToString());
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl11( )
      {
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridusersContainer"+"DivS\" data-gxgridid=\"11\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridusers_Internalname, subGridusers_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridusers_Backcolorstyle == 0 )
            {
               subGridusers_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridusers_Class) > 0 )
               {
                  subGridusers_Linesclass = subGridusers_Class+"Title";
               }
            }
            else
            {
               subGridusers_Titlebackstyle = 1;
               if ( subGridusers_Backcolorstyle == 1 )
               {
                  subGridusers_Titlebackcolor = subGridusers_Allbackcolor;
                  if ( StringUtil.Len( subGridusers_Class) > 0 )
                  {
                     subGridusers_Linesclass = subGridusers_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridusers_Class) > 0 )
                  {
                     subGridusers_Linesclass = subGridusers_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "contact Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( edtavCtlcontactprivatename_Title) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridusersContainer.AddObjectProperty("GridName", "Gridusers");
         }
         else
         {
            GridusersContainer.AddObjectProperty("GridName", "Gridusers");
            GridusersContainer.AddObjectProperty("Header", subGridusers_Header);
            GridusersContainer.AddObjectProperty("Class", "Grid");
            GridusersContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Backcolorstyle), 1, 0, ".", "")));
            GridusersContainer.AddObjectProperty("CmpContext", "");
            GridusersContainer.AddObjectProperty("InMasterPage", "false");
            GridusersColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridusersColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactid_Enabled), 5, 0, ".", "")));
            GridusersContainer.AddColumnProperties(GridusersColumn);
            GridusersColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridusersColumn.AddObjectProperty("Title", StringUtil.RTrim( edtavCtlcontactprivatename_Title));
            GridusersColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0, ".", "")));
            GridusersContainer.AddColumnProperties(GridusersColumn);
            GridusersColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridusersColumn.AddObjectProperty("Value", context.convertURL( AV10deleteImage));
            GridusersColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavDeleteimage_Link));
            GridusersContainer.AddColumnProperties(GridusersColumn);
            GridusersContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Selectedindex), 4, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Allowselection), 1, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Selectioncolor), 9, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Allowhovering), 1, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Hoveringcolor), 9, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Allowcollapsing), 1, 0, ".", "")));
            GridusersContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridusers_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         cmbavUsername_Internalname = "vUSERNAME";
         edtavCtlcontactid_Internalname = "CTLCONTACTID";
         edtavCtlcontactprivatename_Internalname = "CTLCONTACTPRIVATENAME";
         edtavDeleteimage_Internalname = "vDELETEIMAGE";
         bttSave_Internalname = "SAVE";
         bttCancel_Internalname = "CANCEL";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridusers_Internalname = "GRIDUSERS";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridusers_Allowcollapsing = 0;
         subGridusers_Allowselection = 0;
         edtavDeleteimage_Link = "";
         subGridusers_Header = "";
         edtavCtlcontactprivatename_Title = "";
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavCtlcontactprivatename_Jsonclick = "";
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactid_Jsonclick = "";
         edtavCtlcontactid_Enabled = 0;
         subGridusers_Class = "Grid";
         subGridusers_Backcolorstyle = 0;
         edtavCtlcontactprivatename_Title = "";
         edtavCtlcontactprivatename_Enabled = -1;
         edtavCtlcontactid_Enabled = -1;
         cmbavUsername_Jsonclick = "";
         cmbavUsername.Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "WWContacts";
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"GRIDUSERS_nEOF","type":"int"},{"av":"AV42contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV57availableContacts","fld":"vAVAILABLECONTACTS","hsh":true,"type":""},{"av":"AV17groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV59passwordId","fld":"vPASSWORDID","hsh":true,"type":"guid"}]}""");
         setEventMetadata("VUSERNAME.CONTROLVALUECHANGED","""{"handler":"E11312","iparms":[{"av":"AV42contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"cmbavUsername"},{"av":"AV36userName","fld":"vUSERNAME","type":"guid"},{"av":"AV57availableContacts","fld":"vAVAILABLECONTACTS","hsh":true,"type":""},{"av":"GRIDUSERS_nEOF","type":"int"},{"av":"AV17groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV59passwordId","fld":"vPASSWORDID","hsh":true,"type":"guid"}]""");
         setEventMetadata("VUSERNAME.CONTROLVALUECHANGED",""","oparms":[{"av":"AV42contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E12312","iparms":[{"av":"AV17groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV59passwordId","fld":"vPASSWORDID","hsh":true,"type":"guid"},{"av":"AV42contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E13312","iparms":[]}""");
         setEventMetadata("GRIDUSERS.LOAD","""{"handler":"E15312","iparms":[]""");
         setEventMetadata("GRIDUSERS.LOAD",""","oparms":[{"av":"AV10deleteImage","fld":"vDELETEIMAGE","type":"bits"}]}""");
         setEventMetadata("'REMOVE USER'","""{"handler":"E16312","iparms":[{"av":"AV42contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDUSERS_nEOF","type":"int"},{"av":"AV57availableContacts","fld":"vAVAILABLECONTACTS","hsh":true,"type":""},{"av":"AV17groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV59passwordId","fld":"vPASSWORDID","hsh":true,"type":"guid"}]""");
         setEventMetadata("'REMOVE USER'",""","oparms":[{"av":"AV42contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("VALIDV_USERNAME","""{"handler":"Validv_Username","iparms":[]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deleteimage","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         wcpOAV17groupId = Guid.Empty;
         wcpOAV59passwordId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV57availableContacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV42contacts = new GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact>( context, "VaultContact", "distributedcryptography");
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         AV36userName = Guid.Empty;
         GridusersContainer = new GXWebGrid( context);
         sStyleString = "";
         ClassString = "";
         StyleString = "";
         bttSave_Jsonclick = "";
         bttCancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV10deleteImage = "";
         AV66Deleteimage_GXI = "";
         AV13error = "";
         AV58description = "";
         AV56assignedTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV55allTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV19oneContact = new GeneXus.Programs.wallet.SdtVaultContact(context);
         GXt_char1 = "";
         GridusersRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridusers_Linesclass = "";
         ROClassString = "";
         sImgUrl = "";
         GridusersColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlcontactid_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridusers_Backcolorstyle ;
      private short GRIDUSERS_nEOF ;
      private short nGXWrapped ;
      private short subGridusers_Backstyle ;
      private short subGridusers_Titlebackstyle ;
      private short subGridusers_Allowselection ;
      private short subGridusers_Allowhovering ;
      private short subGridusers_Allowcollapsing ;
      private short subGridusers_Collapsed ;
      private int nRC_GXsfl_11 ;
      private int nGXsfl_11_idx=1 ;
      private int AV60GXV1 ;
      private int subGridusers_Islastpage ;
      private int edtavCtlcontactid_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int nGXsfl_11_fel_idx=1 ;
      private int AV63GXV4 ;
      private int AV64GXV5 ;
      private int AV65GXV6 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int AV67GXV7 ;
      private int idxLst ;
      private int subGridusers_Backcolor ;
      private int subGridusers_Allbackcolor ;
      private int subGridusers_Titlebackcolor ;
      private int subGridusers_Selectedindex ;
      private int subGridusers_Selectioncolor ;
      private int subGridusers_Hoveringcolor ;
      private long GRIDUSERS_nCurrentRecord ;
      private long GRIDUSERS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_11_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string cmbavUsername_Internalname ;
      private string TempTags ;
      private string cmbavUsername_Jsonclick ;
      private string sStyleString ;
      private string subGridusers_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttSave_Internalname ;
      private string bttSave_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavDeleteimage_Internalname ;
      private string sGXsfl_11_fel_idx="0001" ;
      private string AV13error ;
      private string AV58description ;
      private string edtavCtlcontactprivatename_Title ;
      private string edtavCtlcontactprivatename_Internalname ;
      private string GXt_char1 ;
      private string edtavDeleteimage_gximage ;
      private string edtavCtlcontactid_Internalname ;
      private string subGridusers_Class ;
      private string subGridusers_Linesclass ;
      private string ROClassString ;
      private string edtavCtlcontactid_Jsonclick ;
      private string edtavCtlcontactprivatename_Jsonclick ;
      private string sImgUrl ;
      private string edtavDeleteimage_Jsonclick ;
      private string subGridusers_Header ;
      private string edtavDeleteimage_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_11_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV11 ;
      private bool AV45found ;
      private bool AV10deleteImage_IsBlob ;
      private string AV66Deleteimage_GXI ;
      private string AV10deleteImage ;
      private Guid AV17groupId ;
      private Guid AV59passwordId ;
      private Guid wcpOAV17groupId ;
      private Guid wcpOAV59passwordId ;
      private Guid AV36userName ;
      private GXWebGrid GridusersContainer ;
      private GXWebRow GridusersRow ;
      private GXWebColumn GridusersColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavUsername ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV57availableContacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtVaultContact> AV42contacts ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV56assignedTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV55allTags ;
      private GeneXus.Programs.wallet.SdtVaultContact AV19oneContact ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
