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

      public void execute( )
      {
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
            gxfirstwebparm = GetNextPar( );
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
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
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
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18onePassword);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridusers_refresh( AV18onePassword) ;
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
         PA2W2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2W2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 310420), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.wwcontacts") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEPASSWORD", AV18onePassword);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEPASSWORD", AV18onePassword);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEPASSWORD", GetSecureSignedToken( "", AV18onePassword, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Contacts", AV7contacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Contacts", AV7contacts);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCONTACTS", AV7contacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCONTACTS", AV7contacts);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV13group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV13group_sdt);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORDS", AV21Passwords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORDS", AV21Passwords);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEPASSWORD", AV18onePassword);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEPASSWORD", AV18onePassword);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEPASSWORD", GetSecureSignedToken( "", AV18onePassword, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vPASSWORDS_AND_TAGS", AV22Passwords_and_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vPASSWORDS_AND_TAGS", AV22Passwords_and_tags);
         }
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
            WE2W2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2W2( ) ;
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
         return formatLink("wallet.registered.wwcontacts")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.WWContacts" ;
      }

      public override string GetPgmdesc( )
      {
         return "WWContacts" ;
      }

      protected void WB2W0( )
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
            GxWebStd.gx_combobox_ctrl1( context, cmbavUsername, cmbavUsername_Internalname, AV24userName.ToString(), 1, cmbavUsername_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "guid", "", 1, cmbavUsername.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", "", true, 0, "HLP_Wallet/registered/WWContacts.htm");
            cmbavUsername.CurrentValue = AV24userName.ToString();
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
               AV29GXV1 = nGXsfl_11_idx;
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
                  AV29GXV1 = nGXsfl_11_idx;
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

      protected void START2W2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186676", 0) ;
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
         STRUP2W0( ) ;
      }

      protected void WS2W2( )
      {
         START2W2( ) ;
         EVT2W2( ) ;
      }

      protected void EVT2W2( )
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
                              E112W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Save' */
                              E122W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E132W2 ();
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
                              AV29GXV1 = nGXsfl_11_idx;
                              if ( ( AV7contacts.Count >= AV29GXV1 ) && ( AV29GXV1 > 0 ) )
                              {
                                 AV7contacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1));
                                 AV8deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)) ? AV39Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV8deleteImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV8deleteImage), true);
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
                                    E142W2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDUSERS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridusers.Load */
                                    E152W2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'REMOVE USER'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Remove User' */
                                    E162W2 ();
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

      protected void WE2W2( )
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

      protected void PA2W2( )
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

      protected void gxgrGridusers_refresh( GeneXus.Programs.wallet.SdtPassword AV18onePassword )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDUSERS_nCurrentRecord = 0;
         RF2W2( ) ;
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
            AV24userName = StringUtil.StrToGuid( cmbavUsername.getValidValue(AV24userName.ToString()));
            AssignAttri("", false, "AV24userName", AV24userName.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUsername.CurrentValue = AV24userName.ToString();
            AssignProp("", false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2W2( ) ;
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

      protected void RF2W2( )
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
            E152W2 ();
            wbEnd = 11;
            WB2W0( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2W2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEPASSWORD", AV18onePassword);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEPASSWORD", AV18onePassword);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEPASSWORD", GetSecureSignedToken( "", AV18onePassword, context));
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

      protected void STRUP2W0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E142W2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Contacts"), AV7contacts);
            ajax_req_read_hidden_sdt(cgiGet( "vCONTACTS"), AV7contacts);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subGridusers_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subGridusers_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV29GXV1 = nGXsfl_11_fel_idx;
               if ( ( AV7contacts.Count >= AV29GXV1 ) && ( AV29GXV1 > 0 ) )
               {
                  AV7contacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1));
                  AV8deleteImage = cgiGet( edtavDeleteimage_Internalname);
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
            AV24userName = StringUtil.StrToGuid( cgiGet( cmbavUsername_Internalname));
            AssignAttri("", false, "AV24userName", AV24userName.ToString());
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
         E142W2 ();
         if (returnInSub) return;
      }

      protected void E142W2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV13group_sdt.FromJSonString(AV25websession.Get("Group_EDIT"), null);
         /* Execute user subroutine: 'READ PASSWORDS_AND_TAGS' */
         S112 ();
         if (returnInSub) return;
         AV21Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV22Passwords_and_tags.gxTpr_Password.Clone());
         cmbavUsername.removeAllItems();
         cmbavUsername.addItem(StringUtil.StrToGuid( "").ToString(), "Select a Contact to Add", 0);
         AV32GXV4 = 1;
         while ( AV32GXV4 <= AV13group_sdt.gxTpr_Contact.Count )
         {
            AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV32GXV4));
            if ( ! (DateTime.MinValue==AV15oneContact.gxTpr_Contactinvitacionaccepted) )
            {
               cmbavUsername.addItem(AV15oneContact.gxTpr_Contactid.ToString(), AV15oneContact.gxTpr_Contactprivatename, 0);
            }
            AV32GXV4 = (int)(AV32GXV4+1);
         }
         AV18onePassword.FromJSonString(AV25websession.Get("ONE_PASSWORD_TO_WWCONTACTS"), null);
         edtavCtlcontactprivatename_Title = "Assigned to "+StringUtil.Trim( AV18onePassword.gxTpr_Description);
         AssignProp("", false, edtavCtlcontactprivatename_Internalname, "Title", edtavCtlcontactprivatename_Title, !bGXsfl_11_Refreshing);
         AV33GXV5 = 1;
         while ( AV33GXV5 <= AV18onePassword.gxTpr_Contact.Count )
         {
            AV16oneContactItem = ((Guid)AV18onePassword.gxTpr_Contact.Item(AV33GXV5));
            AV34GXV6 = 1;
            while ( AV34GXV6 <= AV13group_sdt.gxTpr_Contact.Count )
            {
               AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV34GXV6));
               if ( AV15oneContact.gxTpr_Contactid == AV16oneContactItem )
               {
                  AV7contacts.Add(AV15oneContact, 0);
                  gx_BV11 = true;
               }
               AV34GXV6 = (int)(AV34GXV6+1);
            }
            AV33GXV5 = (int)(AV33GXV5+1);
         }
      }

      protected void E112W2( )
      {
         AV29GXV1 = nGXsfl_11_idx;
         if ( ( AV29GXV1 > 0 ) && ( AV7contacts.Count >= AV29GXV1 ) )
         {
            AV7contacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1));
         }
         /* Username_Controlvaluechanged Routine */
         returnInSub = false;
         AV11found = false;
         AV35GXV7 = 1;
         while ( AV35GXV7 <= AV7contacts.Count )
         {
            AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV35GXV7));
            if ( AV15oneContact.gxTpr_Contactid == AV24userName )
            {
               AV11found = true;
            }
            AV35GXV7 = (int)(AV35GXV7+1);
         }
         if ( ! AV11found && ! (Guid.Empty==AV24userName) )
         {
            AV17oneContactTemp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
            AV17oneContactTemp.gxTpr_Contactid = AV24userName;
            AV36GXV8 = 1;
            while ( AV36GXV8 <= AV13group_sdt.gxTpr_Contact.Count )
            {
               AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV13group_sdt.gxTpr_Contact.Item(AV36GXV8));
               if ( ! (DateTime.MinValue==AV15oneContact.gxTpr_Contactinvitacionaccepted) && ( AV15oneContact.gxTpr_Contactid == AV24userName ) )
               {
                  AV17oneContactTemp.gxTpr_Contactprivatename = StringUtil.Trim( AV15oneContact.gxTpr_Contactprivatename);
                  AV7contacts.Add(AV17oneContactTemp, 0);
                  gx_BV11 = true;
               }
               AV36GXV8 = (int)(AV36GXV8+1);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV7contacts", AV7contacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridusers_refresh( AV18onePassword) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void E122W2( )
      {
         AV29GXV1 = nGXsfl_11_idx;
         if ( ( AV29GXV1 > 0 ) && ( AV7contacts.Count >= AV29GXV1 ) )
         {
            AV7contacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1));
         }
         /* 'Save' Routine */
         returnInSub = false;
         AV37GXV9 = 1;
         while ( AV37GXV9 <= AV21Passwords.Count )
         {
            AV10findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV21Passwords.Item(AV37GXV9));
            if ( AV10findPassword.gxTpr_Passwordid == AV18onePassword.gxTpr_Passwordid )
            {
               AV10findPassword.gxTpr_Contact.Clear();
               AV38GXV10 = 1;
               while ( AV38GXV10 <= AV7contacts.Count )
               {
                  AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV38GXV10));
                  AV10findPassword.gxTpr_Contact.Add(AV15oneContact.gxTpr_Contactid, 0);
                  AV38GXV10 = (int)(AV38GXV10+1);
               }
            }
            AV37GXV9 = (int)(AV37GXV9+1);
         }
         AV22Passwords_and_tags.gxTpr_Password = AV21Passwords;
         new GeneXus.Programs.wallet.savepasswordforgroupusers(context ).execute( ) ;
         /* Execute user subroutine: 'SAVE PASSWORDS_AND_TAGS' */
         S122 ();
         if (returnInSub) return;
         AV25websession.Set("ONE_PASSWORD_TO_WWCONTACTS", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21Passwords", AV21Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV22Passwords_and_tags", AV22Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV13group_sdt", AV13group_sdt);
      }

      protected void E132W2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         AV25websession.Set("ONE_PASSWORD_TO_WWCONTACTS", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      private void E152W2( )
      {
         /* Gridusers_Load Routine */
         returnInSub = false;
         AV29GXV1 = 1;
         while ( AV29GXV1 <= AV7contacts.Count )
         {
            AV7contacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV8deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri("", false, edtavDeleteimage_Internalname, AV8deleteImage);
            AV39Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
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
            AV29GXV1 = (int)(AV29GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E162W2( )
      {
         AV29GXV1 = nGXsfl_11_idx;
         if ( ( AV29GXV1 > 0 ) && ( AV7contacts.Count >= AV29GXV1 ) )
         {
            AV7contacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1));
         }
         /* 'Remove User' Routine */
         returnInSub = false;
         AV40GXV11 = 1;
         while ( AV40GXV11 <= AV7contacts.Count )
         {
            AV15oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV40GXV11));
            if ( AV15oneContact.gxTpr_Contactid == ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV7contacts.CurrentItem)).gxTpr_Contactid )
            {
               AV7contacts.RemoveItem(AV7contacts.IndexOf(AV15oneContact));
               gx_BV11 = true;
            }
            AV40GXV11 = (int)(AV40GXV11+1);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV7contacts", AV7contacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridusers_refresh( AV18onePassword) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void S112( )
      {
         /* 'READ PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV13group_sdt.FromJSonString(AV25websession.Get("Group_EDIT"), null);
         GXt_char1 = AV9error;
         new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV13group_sdt.gxTpr_Encryptedtextshare,  AV13group_sdt.gxTpr_Encpassword, out  AV5clearText, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            if ( StringUtil.StrCmp(AV5clearText, "_empty_") == 0 )
            {
               AV5clearText = "";
            }
            AV22Passwords_and_tags.FromJSonString(AV5clearText, null);
         }
         else
         {
            GX_msglist.addItem(AV9error);
         }
      }

      protected void S122( )
      {
         /* 'SAVE PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV13group_sdt.FromJSonString(AV25websession.Get("Group_EDIT"), null);
         GXt_char1 = AV9error;
         GXt_char2 = AV13group_sdt.gxTpr_Encpassword;
         GXt_char3 = AV13group_sdt.gxTpr_Encryptedtextshare;
         new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV22Passwords_and_tags.ToJSonString(false, true),  "", out  GXt_char2, out  GXt_char3, out  GXt_char1) ;
         AV13group_sdt.gxTpr_Encpassword = GXt_char2;
         AV13group_sdt.gxTpr_Encryptedtextshare = GXt_char3;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GXt_char3 = AV9error;
            new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV13group_sdt,  StringUtil.Trim( AV13group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV14groupId, out  GXt_char3) ;
            AV9error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               GXt_char3 = AV9error;
               new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV13group_sdt, out  GXt_char3) ;
               AV9error = GXt_char3;
            }
            else
            {
               GX_msglist.addItem("There was an error updating group on server: "+AV9error);
            }
         }
         else
         {
            GX_msglist.addItem("There was an error encrypting Password: "+AV9error);
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
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
         PA2W2( ) ;
         WS2W2( ) ;
         WE2W2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20257241623329", true, true);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("wallet/registered/wwcontacts.js", "?20257241623330", false, true);
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
         WB2W0( ) ;
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
         GridusersRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactid_Internalname,((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1)).gxTpr_Contactid.ToString(),((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1)).gxTpr_Contactid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlcontactid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)11,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridusersRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV7contacts.Item(AV29GXV1)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridusersContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV8deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV39Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)) ? AV39Deleteimage_GXI : context.PathToRelativeUrl( AV8deleteImage));
         GridusersRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'REMOVE USER\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV8deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes2W2( ) ;
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
            AV24userName = StringUtil.StrToGuid( cmbavUsername.getValidValue(AV24userName.ToString()));
            AssignAttri("", false, "AV24userName", AV24userName.ToString());
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
            GridusersColumn.AddObjectProperty("Value", context.convertURL( AV8deleteImage));
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"GRIDUSERS_nEOF","type":"int"},{"av":"AV7contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV18onePassword","fld":"vONEPASSWORD","hsh":true,"type":""}]}""");
         setEventMetadata("VUSERNAME.CONTROLVALUECHANGED","""{"handler":"E112W2","iparms":[{"av":"AV7contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"cmbavUsername"},{"av":"AV24userName","fld":"vUSERNAME","type":"guid"},{"av":"AV13group_sdt","fld":"vGROUP_SDT","type":""},{"av":"GRIDUSERS_nEOF","type":"int"},{"av":"AV18onePassword","fld":"vONEPASSWORD","hsh":true,"type":""}]""");
         setEventMetadata("VUSERNAME.CONTROLVALUECHANGED",""","oparms":[{"av":"AV7contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E122W2","iparms":[{"av":"AV21Passwords","fld":"vPASSWORDS","type":""},{"av":"AV18onePassword","fld":"vONEPASSWORD","hsh":true,"type":""},{"av":"AV7contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV22Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""}]""");
         setEventMetadata("'SAVE'",""","oparms":[{"av":"AV21Passwords","fld":"vPASSWORDS","type":""},{"av":"AV22Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV13group_sdt","fld":"vGROUP_SDT","type":""}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E132W2","iparms":[]}""");
         setEventMetadata("GRIDUSERS.LOAD","""{"handler":"E152W2","iparms":[]""");
         setEventMetadata("GRIDUSERS.LOAD",""","oparms":[{"av":"AV8deleteImage","fld":"vDELETEIMAGE","type":"bits"}]}""");
         setEventMetadata("'REMOVE USER'","""{"handler":"E162W2","iparms":[{"av":"AV7contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDUSERS_nEOF","type":"int"},{"av":"AV18onePassword","fld":"vONEPASSWORD","hsh":true,"type":""}]""");
         setEventMetadata("'REMOVE USER'",""","oparms":[{"av":"AV7contacts","fld":"vCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDUSERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDUSERS","prop":"GridRC","grid":11,"type":"int"}]}""");
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
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV18onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV7contacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "distributedcryptography");
         AV13group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV21Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         AV22Passwords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         AV24userName = Guid.Empty;
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
         AV8deleteImage = "";
         AV39Deleteimage_GXI = "";
         AV25websession = context.GetSession();
         AV15oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV16oneContactItem = Guid.Empty;
         AV17oneContactTemp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV10findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         GridusersRow = new GXWebRow();
         AV9error = "";
         AV5clearText = "";
         GXt_char1 = "";
         GXt_char2 = "";
         AV14groupId = Guid.Empty;
         GXt_char3 = "";
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
      private int AV29GXV1 ;
      private int subGridusers_Islastpage ;
      private int edtavCtlcontactid_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int nGXsfl_11_fel_idx=1 ;
      private int AV32GXV4 ;
      private int AV33GXV5 ;
      private int AV34GXV6 ;
      private int AV35GXV7 ;
      private int AV36GXV8 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int AV37GXV9 ;
      private int AV38GXV10 ;
      private int AV40GXV11 ;
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
      private string edtavCtlcontactprivatename_Title ;
      private string edtavCtlcontactprivatename_Internalname ;
      private string edtavDeleteimage_gximage ;
      private string AV9error ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private string GXt_char3 ;
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
      private bool AV11found ;
      private bool AV8deleteImage_IsBlob ;
      private string AV5clearText ;
      private string AV39Deleteimage_GXI ;
      private string AV8deleteImage ;
      private Guid AV24userName ;
      private Guid AV16oneContactItem ;
      private Guid AV14groupId ;
      private IGxSession AV25websession ;
      private GXWebGrid GridusersContainer ;
      private GXWebRow GridusersRow ;
      private GXWebColumn GridusersColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavUsername ;
      private GeneXus.Programs.wallet.SdtPassword AV18onePassword ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV7contacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV13group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV21Passwords ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV22Passwords_and_tags ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV15oneContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV17oneContactTemp ;
      private GeneXus.Programs.wallet.SdtPassword AV10findPassword ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
