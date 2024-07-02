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
namespace GeneXus.Programs.wallet.registered {
   public class chat : GXDataArea
   {
      public chat( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("Wallet.registered.ChatbotDesignSystem", true);
      }

      public chat( IGxContext context )
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridchat") == 0 )
            {
               gxnrGridchat_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridchat") == 0 )
            {
               gxgrGridchat_refresh_invoke( ) ;
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

      protected void gxnrGridchat_newrow_invoke( )
      {
         nRC_GXsfl_14 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_14"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_14_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_14_idx = GetPar( "sGXsfl_14_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridchat_newrow( ) ;
         /* End function gxnrGridchat_newrow_invoke */
      }

      protected void gxgrGridchat_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV10receive_msgs);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV16externalUser);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV21wallet);
         AV12contact.gxTpr_Userprivatename = GetNextPar( );
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridchat_refresh( AV10receive_msgs, AV16externalUser, AV21wallet, AV12contact.gxTpr_Userprivatename) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridchat_refresh_invoke */
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
         this.cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA182( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START182( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2014200), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 2014200), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.chat.aspx") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV16externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV16externalUser);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vEXTERNALUSER", GetSecureSignedToken( "", AV16externalUser, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV21wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV21wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV21wallet, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"Chat");
         forbiddenHiddens.Add("GXV1", StringUtil.RTrim( context.localUtil.Format( AV12contact.gxTpr_Userprivatename, "")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\chat:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Contact", AV12contact);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Contact", AV12contact);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Receive_msgs", AV10receive_msgs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Receive_msgs", AV10receive_msgs);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_14", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_14), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV16externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV16externalUser);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vEXTERNALUSER", GetSecureSignedToken( "", AV16externalUser, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDT_MESSAGE", AV14sdt_message);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDT_MESSAGE", AV14sdt_message);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vJSON_ENC", AV18json_enc);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vJSON_ENC", AV18json_enc);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV21wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV21wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV21wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vRECEIVE_MSGS", AV10receive_msgs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vRECEIVE_MSGS", AV10receive_msgs);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCONTACT", AV12contact);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCONTACT", AV12contact);
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
            WE182( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT182( ) ;
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
         return formatLink("wallet.registered.chat.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.Chat" ;
      }

      public override string GetPgmdesc( )
      {
         return "Chat" ;
      }

      protected void WB180( )
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
            GxWebStd.gx_div_start( context, divChattable_Internalname, 1, 0, "px", 0, "px", "gxchattable", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtluserprivatename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtluserprivatename_Internalname, "Chatting with:", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavCtluserprivatename_Internalname, StringUtil.RTrim( AV12contact.gxTpr_Userprivatename), "", "", 0, 1, edtavCtluserprivatename_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/Chat.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridchatContainer.SetIsFreestyle(true);
            GridchatContainer.SetWrapped(nGXWrapped);
            StartGridControl14( ) ;
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            nRC_GXsfl_14 = (int)(nGXsfl_14_idx-1);
            if ( GridchatContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV23GXV2 = nGXsfl_14_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridchatContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridchat", GridchatContainer, subGridchat_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridchatContainerData", GridchatContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridchatContainerData"+"V", GridchatContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridchatContainerData"+"V"+"\" value='"+GridchatContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_29_182( true) ;
         }
         else
         {
            wb_table1_29_182( false) ;
         }
         return  ;
      }

      protected void wb_table1_29_182e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridchatContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV23GXV2 = nGXsfl_14_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridchatContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridchat", GridchatContainer, subGridchat_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridchatContainerData", GridchatContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridchatContainerData"+"V", GridchatContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridchatContainerData"+"V"+"\" value='"+GridchatContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START182( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_8-180599", 0) ;
            }
         }
         Form.Meta.addItem("description", "Chat", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP180( ) ;
      }

      protected void WS182( )
      {
         START182( ) ;
         EVT182( ) ;
      }

      protected void EVT182( )
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
                           else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS.RECEIVECHAT") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E11182 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SEND'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Send' */
                              E12182 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "GRIDCHAT.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
                              SubsflControlProps_142( ) ;
                              AV23GXV2 = nGXsfl_14_idx;
                              if ( ( AV10receive_msgs.Count >= AV23GXV2 ) && ( AV23GXV2 > 0 ) )
                              {
                                 AV10receive_msgs.CurrentItem = ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2));
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
                                    E13182 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDCHAT.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridchat.Load */
                                    E14182 ();
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

      protected void WE182( )
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

      protected void PA182( )
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
               GX_FocusControl = edtavCtluserprivatename_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridchat_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_142( ) ;
         while ( nGXsfl_14_idx <= nRC_GXsfl_14 )
         {
            sendrow_142( ) ;
            nGXsfl_14_idx = ((subGridchat_Islastpage==1)&&(nGXsfl_14_idx+1>subGridchat_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridchatContainer)) ;
         /* End function gxnrGridchat_newrow */
      }

      protected void gxgrGridchat_refresh( GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_receive_msg> AV10receive_msgs ,
                                           GeneXus.Programs.distcrypt.SdtExternalUser AV16externalUser ,
                                           GeneXus.Programs.wallet.SdtWallet AV21wallet ,
                                           string GXV1 )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDCHAT_nCurrentRecord = 0;
         RF182( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"Chat");
         forbiddenHiddens.Add("GXV1", StringUtil.RTrim( context.localUtil.Format( AV12contact.gxTpr_Userprivatename, "")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\chat:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
         /* End function gxgrGridchat_refresh */
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
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF182( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtluserprivatename_Enabled = 0;
         AssignProp("", false, edtavCtluserprivatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtluserprivatename_Enabled), 5, 0), true);
         edtavCtldatetime_Enabled = 0;
         AssignProp("", false, edtavCtldatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime_Enabled), 5, 0), !bGXsfl_14_Refreshing);
         edtavCtlmsessageenc_Enabled = 0;
         AssignProp("", false, edtavCtlmsessageenc_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlmsessageenc_Enabled), 5, 0), !bGXsfl_14_Refreshing);
      }

      protected void RF182( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridchatContainer.ClearRows();
         }
         wbStart = 14;
         nGXsfl_14_idx = 1;
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         bGXsfl_14_Refreshing = true;
         GridchatContainer.AddObjectProperty("GridName", "Gridchat");
         GridchatContainer.AddObjectProperty("CmpContext", "");
         GridchatContainer.AddObjectProperty("InMasterPage", "false");
         GridchatContainer.AddObjectProperty("Class", StringUtil.RTrim( "gxchatgrid"));
         GridchatContainer.AddObjectProperty("Class", "gxchatgrid");
         GridchatContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridchatContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridchatContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Backcolorstyle), 1, 0, ".", "")));
         GridchatContainer.PageSize = subGridchat_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_142( ) ;
            /* Execute user event: Gridchat.Load */
            E14182 ();
            wbEnd = 14;
            WB180( ) ;
         }
         bGXsfl_14_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes182( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV16externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV16externalUser);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vEXTERNALUSER", GetSecureSignedToken( "", AV16externalUser, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV21wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV21wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV21wallet, context));
      }

      protected int subGridchat_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridchat_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridchat_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridchat_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtluserprivatename_Enabled = 0;
         AssignProp("", false, edtavCtluserprivatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtluserprivatename_Enabled), 5, 0), true);
         edtavCtldatetime_Enabled = 0;
         AssignProp("", false, edtavCtldatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime_Enabled), 5, 0), !bGXsfl_14_Refreshing);
         edtavCtlmsessageenc_Enabled = 0;
         AssignProp("", false, edtavCtlmsessageenc_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlmsessageenc_Enabled), 5, 0), !bGXsfl_14_Refreshing);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP180( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E13182 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vCONTACT"), AV12contact);
            ajax_req_read_hidden_sdt(cgiGet( "Contact"), AV12contact);
            ajax_req_read_hidden_sdt(cgiGet( "Receive_msgs"), AV10receive_msgs);
            ajax_req_read_hidden_sdt(cgiGet( "vRECEIVE_MSGS"), AV10receive_msgs);
            /* Read saved values. */
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_14_fel_idx = 0;
            while ( nGXsfl_14_fel_idx < nRC_GXsfl_14 )
            {
               nGXsfl_14_fel_idx = ((subGridchat_Islastpage==1)&&(nGXsfl_14_fel_idx+1>subGridchat_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_fel_idx+1);
               sGXsfl_14_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_142( ) ;
               AV23GXV2 = nGXsfl_14_fel_idx;
               if ( ( AV10receive_msgs.Count >= AV23GXV2 ) && ( AV23GXV2 > 0 ) )
               {
                  AV10receive_msgs.CurrentItem = ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2));
               }
            }
            if ( nGXsfl_14_fel_idx == 0 )
            {
               nGXsfl_14_idx = 1;
               sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
               SubsflControlProps_142( ) ;
            }
            nGXsfl_14_fel_idx = 1;
            /* Read variables values. */
            AV12contact.gxTpr_Userprivatename = cgiGet( edtavCtluserprivatename_Internalname);
            AV11sendClearText = cgiGet( edtavSendcleartext_Internalname);
            AssignAttri("", false, "AV11sendClearText", AV11sendClearText);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"Chat");
            AV12contact.gxTpr_Userprivatename = cgiGet( edtavCtluserprivatename_Internalname);
            forbiddenHiddens.Add("GXV1", StringUtil.RTrim( context.localUtil.Format( AV12contact.gxTpr_Userprivatename, "")));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\chat:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
               return  ;
            }
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E13182 ();
         if (returnInSub) return;
      }

      protected void E13182( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtContact_SDT1 = AV12contact;
         new GeneXus.Programs.wallet.getcontact(context ).execute( out  GXt_SdtContact_SDT1) ;
         AV12contact = GXt_SdtContact_SDT1;
         if ( (Guid.Empty==AV12contact.gxTpr_Contactrid) )
         {
            GX_msglist.addItem("No contact selected");
         }
         GXt_SdtExternalUser2 = AV16externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV16externalUser = GXt_SdtExternalUser2;
         /* Execute user subroutine: 'CONTACT_CHAT_TO_SCREEN' */
         S112 ();
         if (returnInSub) return;
         GXt_SdtWallet3 = AV21wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet3) ;
         AV21wallet = GXt_SdtWallet3;
      }

      protected void S112( )
      {
         /* 'CONTACT_CHAT_TO_SCREEN' Routine */
         returnInSub = false;
         GXt_objcol_SdtSDT_Chat4 = AV19sdt_all_chats;
         new GeneXus.Programs.wallet.registered.readcontactchat(context ).execute(  AV12contact.gxTpr_Contactrid, out  GXt_objcol_SdtSDT_Chat4) ;
         AV19sdt_all_chats = GXt_objcol_SdtSDT_Chat4;
         AV10receive_msgs = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_receive_msg>( context, "SDT_receive_msg", "distributedcryptography");
         gx_BV14 = true;
         AV26GXV5 = 1;
         while ( AV26GXV5 <= AV19sdt_all_chats.Count )
         {
            AV15sdt_chat = ((GeneXus.Programs.nostr.SdtSDT_Chat)AV19sdt_all_chats.Item(AV26GXV5));
            AV8receive_msg = new GeneXus.Programs.nostr.SdtSDT_receive_msg(context);
            GXt_dtime5 = (DateTime)(DateTime.MinValue);
            new GeneXus.Programs.distributedcrypto.unixtimetolocaldatetime(context ).execute(  AV15sdt_chat.gxTpr_Datetimeunix, out  GXt_dtime5) ;
            AV8receive_msg.gxTpr_Datetime = GXt_dtime5;
            AV8receive_msg.gxTpr_Topic = AV15sdt_chat.gxTpr_From;
            AV8receive_msg.gxTpr_Msessageenc = AV15sdt_chat.gxTpr_Message;
            AV10receive_msgs.Add(AV8receive_msg, 0);
            gx_BV14 = true;
            AV26GXV5 = (int)(AV26GXV5+1);
         }
      }

      protected void E11182( )
      {
         AV23GXV2 = nGXsfl_14_idx;
         if ( ( AV23GXV2 > 0 ) && ( AV10receive_msgs.Count >= AV23GXV2 ) )
         {
            AV10receive_msgs.CurrentItem = ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2));
         }
         /* GlobalEvents_Receivechat Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CONTACT_CHAT_TO_SCREEN' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV14 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV10receive_msgs", AV10receive_msgs);
            nGXsfl_14_bak_idx = nGXsfl_14_idx;
            gxgrGridchat_refresh( AV10receive_msgs, AV16externalUser, AV21wallet, AV12contact.gxTpr_Userprivatename) ;
            nGXsfl_14_idx = nGXsfl_14_bak_idx;
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
      }

      protected void E12182( )
      {
         AV23GXV2 = nGXsfl_14_idx;
         if ( ( AV23GXV2 > 0 ) && ( AV10receive_msgs.Count >= AV23GXV2 ) )
         {
            AV10receive_msgs.CurrentItem = ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2));
         }
         /* 'Send' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11sendClearText)) )
         {
            AV15sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
            AV15sdt_chat.gxTpr_Id = Guid.NewGuid( );
            AV15sdt_chat.gxTpr_From = AV16externalUser.gxTpr_Userinfo.gxTpr_Username;
            AV15sdt_chat.gxTpr_To = AV12contact.gxTpr_Username;
            GXt_int6 = 0;
            new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int6) ;
            AV15sdt_chat.gxTpr_Datetimeunix = GXt_int6;
            AV15sdt_chat.gxTpr_Message = StringUtil.Trim( AV11sendClearText);
            new GeneXus.Programs.wallet.registered.appendcontactchat(context ).execute(  AV12contact.gxTpr_Contactrid,  AV15sdt_chat) ;
            AV14sdt_message.gxTpr_Id = AV15sdt_chat.gxTpr_Id;
            AV14sdt_message.gxTpr_Datetimeunix = AV15sdt_chat.gxTpr_Datetimeunix;
            AV14sdt_message.gxTpr_Messagetype = 50;
            AV14sdt_message.gxTpr_Message = AV15sdt_chat.ToJSonString(false, true);
            GXt_char7 = AV5error;
            GXt_char8 = AV18json_enc.gxTpr_Encryptedkey;
            GXt_char9 = AV18json_enc.gxTpr_Iv;
            GXt_char10 = AV18json_enc.gxTpr_Encryptedtext;
            new GeneXus.Programs.distributedcrypto.encryptjsonto(context ).execute(  AV14sdt_message.ToJSonString(false, true),  AV12contact.gxTpr_Userpubkey, out  GXt_char8, out  GXt_char9, out  GXt_char10, out  GXt_char7) ;
            AV18json_enc.gxTpr_Encryptedkey = GXt_char8;
            AV18json_enc.gxTpr_Iv = GXt_char9;
            AV18json_enc.gxTpr_Encryptedtext = GXt_char10;
            AV5error = GXt_char7;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               GXt_char10 = AV5error;
               new GeneXus.Programs.nbitcoin.tapprootaddresstoxonlypubkey(context ).execute(  AV12contact.gxTpr_Username,  AV21wallet.gxTpr_Networktype, out  AV20taprootPubKey, out  GXt_char10) ;
               AV5error = GXt_char10;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
               {
                  GXt_char10 = AV5error;
                  new GeneXus.Programs.nostr.publish(context ).execute(  AV20taprootPubKey,  AV18json_enc.ToJSonString(false, true), out  GXt_char10) ;
                  AV5error = GXt_char10;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
                  {
                     /* Execute user subroutine: 'CONTACT_CHAT_TO_SCREEN' */
                     S112 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error: ",(string)AV5error}, true);
                  }
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error: ",(string)"Convertin Address to PubKey - "+AV5error}, true);
               }
            }
            else
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error: ",(string)AV5error}, true);
            }
            AV11sendClearText = "";
            AssignAttri("", false, "AV11sendClearText", AV11sendClearText);
            GX_FocusControl = edtavSendcleartext_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            context.DoAjaxSetFocus(GX_FocusControl);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14sdt_message", AV14sdt_message);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV18json_enc", AV18json_enc);
         if ( gx_BV14 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV10receive_msgs", AV10receive_msgs);
            nGXsfl_14_bak_idx = nGXsfl_14_idx;
            gxgrGridchat_refresh( AV10receive_msgs, AV16externalUser, AV21wallet, AV12contact.gxTpr_Userprivatename) ;
            nGXsfl_14_idx = nGXsfl_14_bak_idx;
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
      }

      private void E14182( )
      {
         /* Gridchat_Load Routine */
         returnInSub = false;
         AV23GXV2 = 1;
         while ( AV23GXV2 <= AV10receive_msgs.Count )
         {
            AV10receive_msgs.CurrentItem = ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2));
            if ( StringUtil.StrCmp(((GeneXus.Programs.nostr.SdtSDT_receive_msg)(AV10receive_msgs.CurrentItem)).gxTpr_Topic, StringUtil.Trim( AV16externalUser.gxTpr_Userinfo.gxTpr_Username)) == 0 )
            {
               divGridmessagestable_Class = "gxchatgrid-send-table";
               AssignProp("", false, divGridmessagestable_Internalname, "Class", divGridmessagestable_Class, !bGXsfl_14_Refreshing);
               tblTablemessage_Class = "gxchatgrid-send-message";
               AssignProp("", false, tblTablemessage_Internalname, "Class", tblTablemessage_Class, !bGXsfl_14_Refreshing);
            }
            else
            {
               divGridmessagestable_Class = "gxchatgrid-response-table";
               AssignProp("", false, divGridmessagestable_Internalname, "Class", divGridmessagestable_Class, !bGXsfl_14_Refreshing);
               tblTablemessage_Class = "gxchatgrid-response-message";
               AssignProp("", false, tblTablemessage_Internalname, "Class", tblTablemessage_Class, !bGXsfl_14_Refreshing);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 14;
            }
            sendrow_142( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_14_Refreshing )
            {
               DoAjaxLoad(14, GridchatRow);
            }
            AV23GXV2 = (int)(AV23GXV2+1);
         }
         /*  Sending Event outputs  */
      }

      protected void wb_table1_29_182( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTable1_Internalname, tblTable1_Internalname, "", "gxchatsend-message-table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'',false,'" + sGXsfl_14_idx + "',0)\"";
            ClassString = "gxchatsend-text";
            StyleString = "";
            ClassString = "gxchatsend-text";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSendcleartext_Internalname, AV11sendClearText, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\"", 0, 1, edtavSendcleartext_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2097152", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/Chat.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            /* Active images/pictures */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'',0)\"";
            ClassString = "gxchaticon" + " " + ((StringUtil.StrCmp(imgChatsendimage_gximage, "")==0) ? "GX_Image_ChatSend_Class" : "GX_Image_"+imgChatsendimage_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "a772b4b9-378a-4381-880b-793e6dfdca14", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgChatsendimage_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 5, imgChatsendimage_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'SEND\\'."+"'", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" "+TempTags, "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_Wallet/registered/Chat.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_29_182e( true) ;
         }
         else
         {
            wb_table1_29_182e( false) ;
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
         PA182( ) ;
         WS182( ) ;
         WE182( ) ;
         this.cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024711641378", true, true);
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
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("wallet/registered/chat.js", "?2024711641378", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_142( )
      {
         edtavCtldatetime_Internalname = "CTLDATETIME_"+sGXsfl_14_idx;
         edtavCtlmsessageenc_Internalname = "CTLMSESSAGEENC_"+sGXsfl_14_idx;
      }

      protected void SubsflControlProps_fel_142( )
      {
         edtavCtldatetime_Internalname = "CTLDATETIME_"+sGXsfl_14_fel_idx;
         edtavCtlmsessageenc_Internalname = "CTLMSESSAGEENC_"+sGXsfl_14_fel_idx;
      }

      protected void sendrow_142( )
      {
         SubsflControlProps_142( ) ;
         WB180( ) ;
         GridchatRow = GXWebRow.GetNew(context,GridchatContainer);
         if ( subGridchat_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridchat_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
            {
               subGridchat_Linesclass = subGridchat_Class+"Odd";
            }
         }
         else if ( subGridchat_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridchat_Backstyle = 0;
            subGridchat_Backcolor = subGridchat_Allbackcolor;
            if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
            {
               subGridchat_Linesclass = subGridchat_Class+"Uniform";
            }
         }
         else if ( subGridchat_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridchat_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
            {
               subGridchat_Linesclass = subGridchat_Class+"Odd";
            }
            subGridchat_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subGridchat_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridchat_Backstyle = 1;
            if ( ((int)((nGXsfl_14_idx) % (2))) == 0 )
            {
               subGridchat_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
               {
                  subGridchat_Linesclass = subGridchat_Class+"Even";
               }
            }
            else
            {
               subGridchat_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
               {
                  subGridchat_Linesclass = subGridchat_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subGridchat_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_14_idx+"\">") ;
         }
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divGridmessagestable_Internalname+"_"+sGXsfl_14_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)divGridmessagestable_Class,(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         GridchatRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtldatetime_Internalname,(string)"date Time",(string)"col-sm-3 gxchatgrid-message__dateLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         ROClassString = "gxchatgrid-message__date";
         GridchatRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2)).gxTpr_Datetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2)).gxTpr_Datetime, "99/99/99 99:99"),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldatetime_Jsonclick,(short)0,(string)"gxchatgrid-message__date",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavCtldatetime_Enabled,(short)0,(string)"text",(string)"",(short)17,(string)"chr",(short)1,(string)"row",(short)17,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Table start */
         GridchatRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTablemessage_Internalname+"_"+sGXsfl_14_idx,(short)1,(string)tblTablemessage_Class,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridchatRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridchatRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         GridchatRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtlmsessageenc_Internalname,(string)"msessage Enc",(string)"gx-form-item gxchatgrid-message-textLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Multiple line edit */
         ClassString = "gxchatgrid-message-text";
         StyleString = "";
         ClassString = "gxchatgrid-message-text";
         StyleString = "";
         GridchatRow.AddColumnProperties("html_textarea", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlmsessageenc_Internalname,((GeneXus.Programs.nostr.SdtSDT_receive_msg)AV10receive_msgs.Item(AV23GXV2)).gxTpr_Msessageenc,(string)"",(string)"",(short)0,(short)1,(int)edtavCtlmsessageenc_Enabled,(short)0,(short)80,(string)"chr",(short)10,(string)"row",(short)0,(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"2097152",(short)-1,(short)0,(string)"",(string)"",(short)-1,(bool)true,(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(short)0,(string)""});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("cell");
         }
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("row");
         }
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("table");
         }
         /* End of table */
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         send_integrity_lvl_hashes182( ) ;
         /* End of Columns property logic. */
         GridchatContainer.AddRow(GridchatRow);
         nGXsfl_14_idx = ((subGridchat_Islastpage==1)&&(nGXsfl_14_idx+1>subGridchat_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         /* End function sendrow_142 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl14( )
      {
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridchatContainer"+"DivS\" data-gxgridid=\"14\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridchat_Internalname, subGridchat_Internalname, "", "gxchatgrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            GridchatContainer.AddObjectProperty("GridName", "Gridchat");
         }
         else
         {
            GridchatContainer.AddObjectProperty("GridName", "Gridchat");
            GridchatContainer.AddObjectProperty("Header", subGridchat_Header);
            GridchatContainer.AddObjectProperty("Class", StringUtil.RTrim( "gxchatgrid"));
            GridchatContainer.AddObjectProperty("Class", "gxchatgrid");
            GridchatContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Backcolorstyle), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("CmpContext", "");
            GridchatContainer.AddObjectProperty("InMasterPage", "false");
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldatetime_Enabled), 5, 0, ".", "")));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlmsessageenc_Enabled), 5, 0, ".", "")));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Selectedindex), 4, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Allowselection), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Selectioncolor), 9, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Allowhovering), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Hoveringcolor), 9, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Allowcollapsing), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavCtluserprivatename_Internalname = "CTLUSERPRIVATENAME";
         edtavCtldatetime_Internalname = "CTLDATETIME";
         edtavCtlmsessageenc_Internalname = "CTLMSESSAGEENC";
         tblTablemessage_Internalname = "TABLEMESSAGE";
         divGridmessagestable_Internalname = "GRIDMESSAGESTABLE";
         edtavSendcleartext_Internalname = "vSENDCLEARTEXT";
         imgChatsendimage_Internalname = "CHATSENDIMAGE";
         tblTable1_Internalname = "TABLE1";
         divChattable_Internalname = "CHATTABLE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridchat_Internalname = "GRIDCHAT";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("Wallet.registered.ChatbotDesignSystem", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridchat_Allowcollapsing = 0;
         edtavCtlmsessageenc_Enabled = 0;
         edtavCtldatetime_Jsonclick = "";
         edtavCtldatetime_Enabled = 0;
         subGridchat_Class = "gxchatgrid";
         edtavSendcleartext_Enabled = 1;
         tblTablemessage_Class = "Table";
         divGridmessagestable_Class = "Table";
         subGridchat_Backcolorstyle = 0;
         edtavCtlmsessageenc_Enabled = -1;
         edtavCtldatetime_Enabled = -1;
         edtavCtluserprivatename_Enabled = -1;
         edtavCtluserprivatename_Enabled = 0;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Chat";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDCHAT_nFirstRecordOnPage"},{"av":"GRIDCHAT_nEOF"},{"av":"AV10receive_msgs","fld":"vRECEIVE_MSGS","grid":14},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"GRIDCHAT","prop":"GridRC","grid":14},{"av":"AV16externalUser","fld":"vEXTERNALUSER","hsh":true},{"av":"AV21wallet","fld":"vWALLET","hsh":true},{"av":"GXV1","fld":"CTLUSERPRIVATENAME"}]}""");
         setEventMetadata("GLOBALEVENTS.RECEIVECHAT","""{"handler":"E11182","iparms":[{"av":"AV12contact","fld":"vCONTACT"},{"av":"AV10receive_msgs","fld":"vRECEIVE_MSGS","grid":14},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"GRIDCHAT_nFirstRecordOnPage"},{"av":"nRC_GXsfl_14","ctrl":"GRIDCHAT","prop":"GridRC","grid":14},{"av":"GRIDCHAT_nEOF"},{"av":"AV16externalUser","fld":"vEXTERNALUSER","hsh":true},{"av":"AV21wallet","fld":"vWALLET","hsh":true},{"av":"GXV1","fld":"CTLUSERPRIVATENAME"}]""");
         setEventMetadata("GLOBALEVENTS.RECEIVECHAT",""","oparms":[{"av":"AV10receive_msgs","fld":"vRECEIVE_MSGS","grid":14},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"GRIDCHAT_nFirstRecordOnPage"},{"av":"nRC_GXsfl_14","ctrl":"GRIDCHAT","prop":"GridRC","grid":14}]}""");
         setEventMetadata("'SEND'","""{"handler":"E12182","iparms":[{"av":"AV11sendClearText","fld":"vSENDCLEARTEXT"},{"av":"AV16externalUser","fld":"vEXTERNALUSER","hsh":true},{"av":"AV12contact","fld":"vCONTACT"},{"av":"AV14sdt_message","fld":"vSDT_MESSAGE"},{"av":"AV18json_enc","fld":"vJSON_ENC"},{"av":"AV21wallet","fld":"vWALLET","hsh":true},{"av":"AV10receive_msgs","fld":"vRECEIVE_MSGS","grid":14},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"GRIDCHAT_nFirstRecordOnPage"},{"av":"nRC_GXsfl_14","ctrl":"GRIDCHAT","prop":"GridRC","grid":14},{"av":"GRIDCHAT_nEOF"},{"av":"GXV1","fld":"CTLUSERPRIVATENAME"}]""");
         setEventMetadata("'SEND'",""","oparms":[{"av":"AV14sdt_message","fld":"vSDT_MESSAGE"},{"av":"AV18json_enc","fld":"vJSON_ENC"},{"av":"AV11sendClearText","fld":"vSENDCLEARTEXT"},{"av":"AV10receive_msgs","fld":"vRECEIVE_MSGS","grid":14},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"GRIDCHAT_nFirstRecordOnPage"},{"av":"nRC_GXsfl_14","ctrl":"GRIDCHAT","prop":"GridRC","grid":14}]}""");
         setEventMetadata("GRIDCHAT.LOAD","""{"handler":"E14182","iparms":[{"av":"AV10receive_msgs","fld":"vRECEIVE_MSGS","grid":14},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"GRIDCHAT_nFirstRecordOnPage"},{"av":"nRC_GXsfl_14","ctrl":"GRIDCHAT","prop":"GridRC","grid":14},{"av":"AV16externalUser","fld":"vEXTERNALUSER","hsh":true}]""");
         setEventMetadata("GRIDCHAT.LOAD",""","oparms":[{"av":"divGridmessagestable_Class","ctrl":"GRIDMESSAGESTABLE","prop":"Class"},{"av":"tblTablemessage_Class","ctrl":"TABLEMESSAGE","prop":"Class"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv4","iparms":[]}""");
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
         AV10receive_msgs = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_receive_msg>( context, "SDT_receive_msg", "distributedcryptography");
         AV16externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV21wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV12contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV14sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV18json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         GridchatContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV11sendClearText = "";
         hsh = "";
         GXt_SdtContact_SDT1 = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtWallet3 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV19sdt_all_chats = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         GXt_objcol_SdtSDT_Chat4 = new GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat>( context, "SDT_Chat", "distributedcryptography");
         AV15sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         AV8receive_msg = new GeneXus.Programs.nostr.SdtSDT_receive_msg(context);
         GXt_dtime5 = (DateTime)(DateTime.MinValue);
         AV5error = "";
         GXt_char7 = "";
         GXt_char8 = "";
         GXt_char9 = "";
         AV20taprootPubKey = "";
         GXt_char10 = "";
         GridchatRow = new GXWebRow();
         TempTags = "";
         imgChatsendimage_gximage = "";
         sImgUrl = "";
         imgChatsendimage_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridchat_Linesclass = "";
         ROClassString = "";
         subGridchat_Header = "";
         GridchatColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtluserprivatename_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavCtlmsessageenc_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridchat_Backcolorstyle ;
      private short GRIDCHAT_nEOF ;
      private short nGXWrapped ;
      private short subGridchat_Backstyle ;
      private short subGridchat_Allowselection ;
      private short subGridchat_Allowhovering ;
      private short subGridchat_Allowcollapsing ;
      private short subGridchat_Collapsed ;
      private int nRC_GXsfl_14 ;
      private int nGXsfl_14_idx=1 ;
      private int edtavCtluserprivatename_Enabled ;
      private int AV23GXV2 ;
      private int subGridchat_Islastpage ;
      private int edtavCtldatetime_Enabled ;
      private int edtavCtlmsessageenc_Enabled ;
      private int nGXsfl_14_fel_idx=1 ;
      private int AV26GXV5 ;
      private int nGXsfl_14_bak_idx=1 ;
      private int edtavSendcleartext_Enabled ;
      private int idxLst ;
      private int subGridchat_Backcolor ;
      private int subGridchat_Allbackcolor ;
      private int subGridchat_Selectedindex ;
      private int subGridchat_Selectioncolor ;
      private int subGridchat_Hoveringcolor ;
      private long GRIDCHAT_nCurrentRecord ;
      private long GRIDCHAT_nFirstRecordOnPage ;
      private long GXt_int6 ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_14_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string divChattable_Internalname ;
      private string edtavCtluserprivatename_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string sStyleString ;
      private string subGridchat_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavCtldatetime_Internalname ;
      private string edtavCtlmsessageenc_Internalname ;
      private string sGXsfl_14_fel_idx="0001" ;
      private string edtavSendcleartext_Internalname ;
      private string hsh ;
      private string AV5error ;
      private string GXt_char7 ;
      private string GXt_char8 ;
      private string GXt_char9 ;
      private string AV20taprootPubKey ;
      private string GXt_char10 ;
      private string divGridmessagestable_Class ;
      private string divGridmessagestable_Internalname ;
      private string tblTablemessage_Class ;
      private string tblTablemessage_Internalname ;
      private string tblTable1_Internalname ;
      private string TempTags ;
      private string imgChatsendimage_gximage ;
      private string sImgUrl ;
      private string imgChatsendimage_Internalname ;
      private string imgChatsendimage_Jsonclick ;
      private string subGridchat_Class ;
      private string subGridchat_Linesclass ;
      private string ROClassString ;
      private string edtavCtldatetime_Jsonclick ;
      private string subGridchat_Header ;
      private DateTime GXt_dtime5 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_14_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV14 ;
      private string AV11sendClearText ;
      private GXProperties forbiddenHiddens ;
      private GXWebGrid GridchatContainer ;
      private GXWebRow GridchatRow ;
      private GXWebColumn GridchatColumn ;
      private IGxDataStore dsDefault ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_receive_msg> AV10receive_msgs ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> AV19sdt_all_chats ;
      private GXBaseCollection<GeneXus.Programs.nostr.SdtSDT_Chat> GXt_objcol_SdtSDT_Chat4 ;
      private GXWebForm Form ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV12contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT GXt_SdtContact_SDT1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV16externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV18json_enc ;
      private GeneXus.Programs.nostr.SdtSDT_receive_msg AV8receive_msg ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV15sdt_chat ;
      private GeneXus.Programs.nostr.SdtSDT_message AV14sdt_message ;
      private GeneXus.Programs.wallet.SdtWallet AV21wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet3 ;
   }

}
