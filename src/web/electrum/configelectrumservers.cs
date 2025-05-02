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
namespace GeneXus.Programs.electrum {
   public class configelectrumservers : GXWebComponent
   {
      public configelectrumservers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public configelectrumservers( IGxContext context )
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

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
         cmbavCtlnetworktype = new GXCombobox();
         cmbavCtlconnectiontype = new GXCombobox();
         chkavCtlsecure = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Grid1") == 0 )
               {
                  gxnrGrid1_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Grid1") == 0 )
               {
                  gxgrGrid1_refresh_invoke( ) ;
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGrid1_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGrid1_newrow( ) ;
         /* End function gxnrGrid1_newrow_invoke */
      }

      protected void gxgrGrid1_refresh_invoke( )
      {
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid1_refresh( sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid1_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA222( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               cmbavCtlnetworktype.Enabled = 0;
               AssignProp(sPrefix, false, cmbavCtlnetworktype_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavCtlnetworktype.Enabled), 5, 0), !bGXsfl_9_Refreshing);
               WS222( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
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

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "Config Electrum Servers") ;
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
         }
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 123260), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("electrum.configelectrumservers") +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Connectionparameters", AV5ConnectionParameters);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Connectionparameters", AV5ConnectionParameters);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vCONNECTIONPARAMETERS", AV5ConnectionParameters);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vCONNECTIONPARAMETERS", AV5ConnectionParameters);
         }
      }

      protected void RenderHtmlCloseForm222( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Electrum.ConfigElectrumServers" ;
      }

      public override string GetPgmdesc( )
      {
         return "Config Electrum Servers" ;
      }

      protected void WB220( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "electrum.configelectrumservers");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTitle_Internalname, "Electrum Servers Configuration", "", "", lblTitle_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextblockLarge", 0, "", 1, 1, 0, 0, "HLP_Electrum/ConfigElectrumServers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            Grid1Container.SetWrapped(nGXWrapped);
            StartGridControl9( ) ;
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            nRC_GXsfl_9 = (int)(nGXsfl_9_idx-1);
            if ( Grid1Container.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV9GXV1 = nGXsfl_9_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"Grid1Container"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Grid1", Grid1Container, subGrid1_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"Grid1ContainerData", Grid1Container.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"Grid1ContainerData"+"V", Grid1Container.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"Grid1ContainerData"+"V"+"\" value='"+Grid1Container.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSavechanges_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Save Changes", bttSavechanges_Jsonclick, 5, "Save Changes", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SAVE CHANGES\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Electrum/ConfigElectrumServers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Electrum/ConfigElectrumServers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRestoredefaults_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Restore Defaults", bttRestoredefaults_Jsonclick, 5, "Restore Defaults", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'RESTORE DEFAULTS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Electrum/ConfigElectrumServers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( Grid1Container.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV9GXV1 = nGXsfl_9_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"Grid1Container"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Grid1", Grid1Container, subGrid1_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"Grid1ContainerData", Grid1Container.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"Grid1ContainerData"+"V", Grid1Container.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"Grid1ContainerData"+"V"+"\" value='"+Grid1Container.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START222( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
               }
            }
            Form.Meta.addItem("description", "Config Electrum Servers", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP220( ) ;
            }
         }
      }

      protected void WS222( )
      {
         START222( ) ;
         EVT222( ) ;
      }

      protected void EVT222( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP220( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE CHANGES'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP220( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Save Changes' */
                                    E11222 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP220( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Cancel' */
                                    E12222 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RESTORE DEFAULTS'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP220( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Restore Defaults' */
                                    E13222 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP220( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "'EDIT CONNECTION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 10), "GRID1.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP220( ) ;
                              }
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV9GXV1 = nGXsfl_9_idx;
                              if ( ( AV5ConnectionParameters.Count >= AV9GXV1 ) && ( AV9GXV1 > 0 ) )
                              {
                                 AV5ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1));
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          /* Execute user event: Start */
                                          E14222 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'EDIT CONNECTION'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          /* Execute user event: 'Edit Connection' */
                                          E15222 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID1.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          /* Execute user event: Grid1.Load */
                                          E16222 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          if ( ! wbErr )
                                          {
                                             Rfr0gs = false;
                                             if ( ! Rfr0gs )
                                             {
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP220( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                       }
                                    }
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

      protected void WE222( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm222( ) ;
            }
         }
      }

      protected void PA222( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGrid1_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_92( ) ;
         while ( nGXsfl_9_idx <= nRC_GXsfl_9 )
         {
            sendrow_92( ) ;
            nGXsfl_9_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_9_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( Grid1Container)) ;
         /* End function gxnrGrid1_newrow */
      }

      protected void gxgrGrid1_refresh( string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID1_nCurrentRecord = 0;
         RF222( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid1_refresh */
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
         RF222( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         cmbavCtlnetworktype.Enabled = 0;
      }

      protected void RF222( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            Grid1Container.ClearRows();
         }
         wbStart = 9;
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         Grid1Container.AddObjectProperty("GridName", "Grid1");
         Grid1Container.AddObjectProperty("CmpContext", sPrefix);
         Grid1Container.AddObjectProperty("InMasterPage", "false");
         Grid1Container.AddObjectProperty("Class", "Grid");
         Grid1Container.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         Grid1Container.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         Grid1Container.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Backcolorstyle), 1, 0, ".", "")));
         Grid1Container.PageSize = subGrid1_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_92( ) ;
            /* Execute user event: Grid1.Load */
            E16222 ();
            wbEnd = 9;
            WB220( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes222( )
      {
      }

      protected int subGrid1_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         cmbavCtlnetworktype.Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP220( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14222 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Connectionparameters"), AV5ConnectionParameters);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vCONNECTIONPARAMETERS"), AV5ConnectionParameters);
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_9_fel_idx = 0;
            while ( nGXsfl_9_fel_idx < nRC_GXsfl_9 )
            {
               nGXsfl_9_fel_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_9_fel_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_fel_idx+1);
               sGXsfl_9_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_92( ) ;
               AV9GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV5ConnectionParameters.Count >= AV9GXV1 ) && ( AV9GXV1 > 0 ) )
               {
                  AV5ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1));
               }
            }
            if ( nGXsfl_9_fel_idx == 0 )
            {
               nGXsfl_9_idx = 1;
               sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
               SubsflControlProps_92( ) ;
            }
            nGXsfl_9_fel_idx = 1;
            /* Read variables values. */
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
         E14222 ();
         if (returnInSub) return;
      }

      protected void E14222( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 = AV5ConnectionParameters;
         new GeneXus.Programs.electrum.getelectrumconfigservers(context ).execute( out  GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1) ;
         AV5ConnectionParameters = GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1;
         gx_BV9 = true;
      }

      protected void E15222( )
      {
         AV9GXV1 = nGXsfl_9_idx;
         if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) )
         {
            AV5ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1));
         }
         /* 'Edit Connection' Routine */
         returnInSub = false;
         GX_msglist.addItem(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)(AV5ConnectionParameters.CurrentItem)).ToJSonString(false, true));
      }

      protected void E11222( )
      {
         AV9GXV1 = nGXsfl_9_idx;
         if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) )
         {
            AV5ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1));
         }
         /* 'Save Changes' Routine */
         returnInSub = false;
         AV8directory.Source = "Wallets";
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         AV7configFile.Source = "Wallets"+(GXt_boolean3 ? "/" : "\\")+"electrum.conf";
         AV7configFile.WriteAllText(AV5ConnectionParameters.ToJSonString(false), "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void E12222( )
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

      protected void E13222( )
      {
         AV9GXV1 = nGXsfl_9_idx;
         if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) )
         {
            AV5ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1));
         }
         /* 'Restore Defaults' Routine */
         returnInSub = false;
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 = AV5ConnectionParameters;
         new GeneXus.Programs.electrum.defaultparameters(context ).execute( out  GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1) ;
         AV5ConnectionParameters = GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1;
         gx_BV9 = true;
         AV8directory.Source = "Wallets";
         GXt_boolean3 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
         GXt_boolean2 = false;
         new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
         AV7configFile.Source = "Wallets"+(GXt_boolean2 ? "/" : "\\")+"electrum.conf";
         AV7configFile.WriteAllText(AV5ConnectionParameters.ToJSonString(false), "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV5ConnectionParameters", AV5ConnectionParameters);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGrid1_refresh( sPrefix) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      private void E16222( )
      {
         /* Grid1_Load Routine */
         returnInSub = false;
         AV9GXV1 = 1;
         while ( AV9GXV1 <= AV5ConnectionParameters.Count )
         {
            AV5ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 9;
            }
            sendrow_92( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_9_Refreshing )
            {
               DoAjaxLoad(9, Grid1Row);
            }
            AV9GXV1 = (int)(AV9GXV1+1);
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
         PA222( ) ;
         WS222( ) ;
         WE222( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA222( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "electrum\\configelectrumservers", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA222( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
         }
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA222( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS222( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS222( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE222( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2025521316534", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("electrum/configelectrumservers.js", "?2025521316534", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         cmbavCtlnetworktype_Internalname = sPrefix+"CTLNETWORKTYPE_"+sGXsfl_9_idx;
         cmbavCtlconnectiontype_Internalname = sPrefix+"CTLCONNECTIONTYPE_"+sGXsfl_9_idx;
         edtavCtlhostname_Internalname = sPrefix+"CTLHOSTNAME_"+sGXsfl_9_idx;
         edtavCtlport_Internalname = sPrefix+"CTLPORT_"+sGXsfl_9_idx;
         chkavCtlsecure_Internalname = sPrefix+"CTLSECURE_"+sGXsfl_9_idx;
         edtavCtltimeoutmiliseconds_Internalname = sPrefix+"CTLTIMEOUTMILISECONDS_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         cmbavCtlnetworktype_Internalname = sPrefix+"CTLNETWORKTYPE_"+sGXsfl_9_fel_idx;
         cmbavCtlconnectiontype_Internalname = sPrefix+"CTLCONNECTIONTYPE_"+sGXsfl_9_fel_idx;
         edtavCtlhostname_Internalname = sPrefix+"CTLHOSTNAME_"+sGXsfl_9_fel_idx;
         edtavCtlport_Internalname = sPrefix+"CTLPORT_"+sGXsfl_9_fel_idx;
         chkavCtlsecure_Internalname = sPrefix+"CTLSECURE_"+sGXsfl_9_fel_idx;
         edtavCtltimeoutmiliseconds_Internalname = sPrefix+"CTLTIMEOUTMILISECONDS_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB220( ) ;
         Grid1Row = GXWebRow.GetNew(context,Grid1Container);
         if ( subGrid1_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGrid1_Backstyle = 0;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Odd";
            }
         }
         else if ( subGrid1_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGrid1_Backstyle = 0;
            subGrid1_Backcolor = subGrid1_Allbackcolor;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Uniform";
            }
         }
         else if ( subGrid1_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGrid1_Backstyle = 1;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Odd";
            }
            subGrid1_Backcolor = (int)(0x0);
         }
         else if ( subGrid1_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGrid1_Backstyle = 1;
            if ( ((int)((nGXsfl_9_idx) % (2))) == 0 )
            {
               subGrid1_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Even";
               }
            }
            else
            {
               subGrid1_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Odd";
               }
            }
         }
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_9_idx+"\">") ;
         }
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         if ( ( cmbavCtlnetworktype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "CTLNETWORKTYPE_" + sGXsfl_9_idx;
            cmbavCtlnetworktype.Name = GXCCtl;
            cmbavCtlnetworktype.WebTags = "";
            cmbavCtlnetworktype.addItem("MainNet", "MainNet", 0);
            cmbavCtlnetworktype.addItem("TestNet", "TestNet (for testing only)", 0);
            cmbavCtlnetworktype.addItem("RegTest", "RegTest (for testing only)", 0);
            if ( cmbavCtlnetworktype.ItemCount > 0 )
            {
               if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Networktype)) )
               {
                  ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Networktype = cmbavCtlnetworktype.getValidValue(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Networktype);
               }
            }
         }
         /* ComboBox */
         Grid1Row.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavCtlnetworktype,(string)cmbavCtlnetworktype_Internalname,StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Networktype),(short)1,(string)cmbavCtlnetworktype_Jsonclick,(short)0,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"char",(string)"",(short)-1,cmbavCtlnetworktype.Enabled,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"",(string)"",(bool)true,(short)0});
         cmbavCtlnetworktype.CurrentValue = StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Networktype);
         AssignProp(sPrefix, false, cmbavCtlnetworktype_Internalname, "Values", (string)(cmbavCtlnetworktype.ToJavascriptSource()), !bGXsfl_9_Refreshing);
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         if ( ( cmbavCtlconnectiontype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "CTLCONNECTIONTYPE_" + sGXsfl_9_idx;
            cmbavCtlconnectiontype.Name = GXCCtl;
            cmbavCtlconnectiontype.WebTags = "";
            cmbavCtlconnectiontype.addItem("ws", "Web Socket", 0);
            cmbavCtlconnectiontype.addItem("tcp", "Tcp Socket", 0);
            if ( cmbavCtlconnectiontype.ItemCount > 0 )
            {
               if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Connectiontype)) )
               {
                  ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Connectiontype = cmbavCtlconnectiontype.getValidValue(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Connectiontype);
               }
            }
         }
         /* ComboBox */
         Grid1Row.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavCtlconnectiontype,(string)cmbavCtlconnectiontype_Internalname,StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Connectiontype),(short)1,(string)cmbavCtlconnectiontype_Jsonclick,(short)0,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"char",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,11);\"",(string)"",(bool)true,(short)0});
         cmbavCtlconnectiontype.CurrentValue = StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Connectiontype);
         AssignProp(sPrefix, false, cmbavCtlconnectiontype_Internalname, "Values", (string)(cmbavCtlconnectiontype.ToJavascriptSource()), !bGXsfl_9_Refreshing);
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         Grid1Row.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlhostname_Internalname,StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Hostname),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlhostname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(short)1,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         Grid1Row.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlport_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Port), 6, 0, ".", "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Port), "ZZZZZ9"))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,13);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlport_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(short)1,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)6,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ClassString = "Attribute";
         StyleString = "";
         GXCCtl = "CTLSECURE_" + sGXsfl_9_idx;
         chkavCtlsecure.Name = GXCCtl;
         chkavCtlsecure.WebTags = "";
         chkavCtlsecure.Caption = "";
         AssignProp(sPrefix, false, chkavCtlsecure_Internalname, "TitleCaption", chkavCtlsecure.Caption, !bGXsfl_9_Refreshing);
         chkavCtlsecure.CheckedValue = "false";
         Grid1Row.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkavCtlsecure_Internalname,StringUtil.BoolToStr( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Secure),(string)"",(string)"",(short)-1,(short)1,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(14, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,14);\""});
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         Grid1Row.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltimeoutmiliseconds_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Timeoutmiliseconds), 6, 0, ".", "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Timeoutmiliseconds), "ZZZZZ9"))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,15);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltimeoutmiliseconds_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(short)1,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)6,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         send_integrity_lvl_hashes222( ) ;
         Grid1Container.AddRow(Grid1Row);
         nGXsfl_9_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_9_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         /* End function sendrow_92 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "CTLNETWORKTYPE_" + sGXsfl_9_idx;
         cmbavCtlnetworktype.Name = GXCCtl;
         cmbavCtlnetworktype.WebTags = "";
         cmbavCtlnetworktype.addItem("MainNet", "MainNet", 0);
         cmbavCtlnetworktype.addItem("TestNet", "TestNet (for testing only)", 0);
         cmbavCtlnetworktype.addItem("RegTest", "RegTest (for testing only)", 0);
         if ( cmbavCtlnetworktype.ItemCount > 0 )
         {
            if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Networktype)) )
            {
            }
         }
         GXCCtl = "CTLCONNECTIONTYPE_" + sGXsfl_9_idx;
         cmbavCtlconnectiontype.Name = GXCCtl;
         cmbavCtlconnectiontype.WebTags = "";
         cmbavCtlconnectiontype.addItem("ws", "Web Socket", 0);
         cmbavCtlconnectiontype.addItem("tcp", "Tcp Socket", 0);
         if ( cmbavCtlconnectiontype.ItemCount > 0 )
         {
            if ( ( AV9GXV1 > 0 ) && ( AV5ConnectionParameters.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV5ConnectionParameters.Item(AV9GXV1)).gxTpr_Connectiontype)) )
            {
            }
         }
         GXCCtl = "CTLSECURE_" + sGXsfl_9_idx;
         chkavCtlsecure.Name = GXCCtl;
         chkavCtlsecure.WebTags = "";
         chkavCtlsecure.Caption = "";
         AssignProp(sPrefix, false, chkavCtlsecure_Internalname, "TitleCaption", chkavCtlsecure.Caption, !bGXsfl_9_Refreshing);
         chkavCtlsecure.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void StartGridControl9( )
      {
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"Grid1Container"+"DivS\" data-gxgridid=\"9\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid1_Internalname, subGrid1_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGrid1_Backcolorstyle == 0 )
            {
               subGrid1_Titlebackstyle = 0;
               if ( StringUtil.Len( subGrid1_Class) > 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Title";
               }
            }
            else
            {
               subGrid1_Titlebackstyle = 1;
               if ( subGrid1_Backcolorstyle == 1 )
               {
                  subGrid1_Titlebackcolor = subGrid1_Allbackcolor;
                  if ( StringUtil.Len( subGrid1_Class) > 0 )
                  {
                     subGrid1_Linesclass = subGrid1_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGrid1_Class) > 0 )
                  {
                     subGrid1_Linesclass = subGrid1_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Network Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Connection Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Host Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Port") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Secure") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Time Out (miliseconds)") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            Grid1Container.AddObjectProperty("GridName", "Grid1");
         }
         else
         {
            Grid1Container.AddObjectProperty("GridName", "Grid1");
            Grid1Container.AddObjectProperty("Header", subGrid1_Header);
            Grid1Container.AddObjectProperty("Class", "Grid");
            Grid1Container.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Backcolorstyle), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("CmpContext", sPrefix);
            Grid1Container.AddObjectProperty("InMasterPage", "false");
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Link", StringUtil.RTrim( cmbavCtlnetworktype.Link));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavCtlnetworktype.Enabled), 5, 0, ".", "")));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Container.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Selectedindex), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowselection), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Selectioncolor), 9, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowhovering), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Hoveringcolor), 9, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowcollapsing), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTitle_Internalname = sPrefix+"TITLE";
         cmbavCtlnetworktype_Internalname = sPrefix+"CTLNETWORKTYPE";
         cmbavCtlconnectiontype_Internalname = sPrefix+"CTLCONNECTIONTYPE";
         edtavCtlhostname_Internalname = sPrefix+"CTLHOSTNAME";
         edtavCtlport_Internalname = sPrefix+"CTLPORT";
         chkavCtlsecure_Internalname = sPrefix+"CTLSECURE";
         edtavCtltimeoutmiliseconds_Internalname = sPrefix+"CTLTIMEOUTMILISECONDS";
         bttSavechanges_Internalname = sPrefix+"SAVECHANGES";
         bttCancel_Internalname = sPrefix+"CANCEL";
         bttRestoredefaults_Internalname = sPrefix+"RESTOREDEFAULTS";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGrid1_Internalname = sPrefix+"GRID1";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         subGrid1_Allowcollapsing = 0;
         subGrid1_Allowselection = 0;
         cmbavCtlnetworktype.Link = "";
         subGrid1_Header = "";
         edtavCtltimeoutmiliseconds_Jsonclick = "";
         chkavCtlsecure.Caption = "";
         edtavCtlport_Jsonclick = "";
         edtavCtlhostname_Jsonclick = "";
         cmbavCtlconnectiontype_Jsonclick = "";
         cmbavCtlnetworktype_Jsonclick = "";
         cmbavCtlnetworktype.Enabled = 0;
         subGrid1_Class = "Grid";
         subGrid1_Backcolorstyle = 0;
         cmbavCtlnetworktype.Enabled = -1;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID1_nFirstRecordOnPage","type":"int"},{"av":"GRID1_nEOF","type":"int"},{"av":"AV5ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRID1","prop":"GridRC","grid":9,"type":"int"},{"av":"sPrefix","type":"char"}]}""");
         setEventMetadata("'EDIT CONNECTION'","""{"handler":"E15222","iparms":[{"av":"AV5ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRID1_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRID1","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("'SAVE CHANGES'","""{"handler":"E11222","iparms":[{"av":"AV5ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRID1_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRID1","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E12222","iparms":[]}""");
         setEventMetadata("'RESTORE DEFAULTS'","""{"handler":"E13222","iparms":[{"av":"AV5ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRID1_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRID1","prop":"GridRC","grid":9,"type":"int"},{"av":"GRID1_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'RESTORE DEFAULTS'",""","oparms":[{"av":"AV5ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRID1_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRID1","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("VALIDV_GXV3","""{"handler":"Validv_Gxv3","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv7","iparms":[]}""");
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
         sPrefix = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV5ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         GX_FocusControl = "";
         lblTitle_Jsonclick = "";
         Grid1Container = new GXWebGrid( context);
         sStyleString = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttSavechanges_Jsonclick = "";
         bttCancel_Jsonclick = "";
         bttRestoredefaults_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV8directory = new GxDirectory(context.GetPhysicalPath());
         AV7configFile = new GxFile(context.GetPhysicalPath());
         GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         Grid1Row = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid1_Linesclass = "";
         GXCCtl = "";
         ROClassString = "";
         Grid1Column = new GXWebColumn();
         /* GeneXus formulas. */
         cmbavCtlnetworktype.Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid1_Backcolorstyle ;
      private short GRID1_nEOF ;
      private short nGXWrapped ;
      private short subGrid1_Backstyle ;
      private short subGrid1_Titlebackstyle ;
      private short subGrid1_Allowselection ;
      private short subGrid1_Allowhovering ;
      private short subGrid1_Allowcollapsing ;
      private short subGrid1_Collapsed ;
      private int nRC_GXsfl_9 ;
      private int nGXsfl_9_idx=1 ;
      private int AV9GXV1 ;
      private int subGrid1_Islastpage ;
      private int nGXsfl_9_fel_idx=1 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int idxLst ;
      private int subGrid1_Backcolor ;
      private int subGrid1_Allbackcolor ;
      private int subGrid1_Titlebackcolor ;
      private int subGrid1_Selectedindex ;
      private int subGrid1_Selectioncolor ;
      private int subGrid1_Hoveringcolor ;
      private long GRID1_nCurrentRecord ;
      private long GRID1_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_9_idx="0001" ;
      private string cmbavCtlnetworktype_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string lblTitle_Internalname ;
      private string lblTitle_Jsonclick ;
      private string sStyleString ;
      private string subGrid1_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttSavechanges_Internalname ;
      private string bttSavechanges_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string bttRestoredefaults_Internalname ;
      private string bttRestoredefaults_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string cmbavCtlconnectiontype_Internalname ;
      private string edtavCtlhostname_Internalname ;
      private string edtavCtlport_Internalname ;
      private string chkavCtlsecure_Internalname ;
      private string edtavCtltimeoutmiliseconds_Internalname ;
      private string subGrid1_Class ;
      private string subGrid1_Linesclass ;
      private string GXCCtl ;
      private string cmbavCtlnetworktype_Jsonclick ;
      private string cmbavCtlconnectiontype_Jsonclick ;
      private string ROClassString ;
      private string edtavCtlhostname_Jsonclick ;
      private string edtavCtlport_Jsonclick ;
      private string edtavCtltimeoutmiliseconds_Jsonclick ;
      private string subGrid1_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV9 ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean2 ;
      private GXWebGrid Grid1Container ;
      private GXWebRow Grid1Row ;
      private GXWebColumn Grid1Column ;
      private GXWebForm Form ;
      private GxFile AV7configFile ;
      private GxDirectory AV8directory ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavCtlnetworktype ;
      private GXCombobox cmbavCtlconnectiontype ;
      private GXCheckbox chkavCtlsecure ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV5ConnectionParameters ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> GXt_objcol_SdtConnectionParameters_ConnectionParametersItem1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
