var __spreadArray=this&&this.__spreadArray||function(r,n,t){if(t||arguments.length===2)for(var i=0,a=n.length,e;i<a;i++){if(e||!(i in n)){if(!e)e=Array.prototype.slice.call(n,0,i);e[i]=n[i]}}return r.concat(e||Array.prototype.slice.call(n))};System.register([],(function(r){"use strict";return{execute:function(){var n=/^\s*(var\(|url\()/;var t=r("i",(function(){return/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)}));var i=r("R",null);var a=r("m",(function(r){return navigator.clipboard.writeText(r)}));var e=r("t",(function(r){var n=Object.keys(r);var t="";for(var i=0;i<n.length;i++){var a=n[i];var e=r[a];if(e){t+=t===""?a:" ".concat(a)}}return t}));var u=r("h",(function(r,n){var t=Object.keys(r);var i="";for(var a=0;a<t.length;a++){var e=t[a];var u=r[e];if(u){i+=i===""?"".concat(n,":").concat(e):",".concat(n,":").concat(e)}}return i}));var f=r("e",(function(r,n,t){return r<=n&&n<=t}));var v;if(!window.Image){v="test"}v!==null&&v!==void 0?v:v=new Image;var o=r("l",(function(r){return r.dataTransfer.setDragImage(v,0,0)}));var c=r("g",(function(r,n){return r&&n!=="img"}));var s=r("f",(function(r,n,t){return Number.isNaN(t)?Math.max(r,n):Math.max(Math.min(r,t),n)}));var d=r("c",(function(r){document.documentElement.style.cursor=r}));var m=r("r",(function(){document.documentElement.style.cursor=null}));var l=r("d",(function(){document.body.style.pointerEvents="none"}));var g=r("b",(function(){document.body.style.pointerEvents=null}));var b=r("a",(function(){return document.documentElement.dir==="rtl"}));var h;var y;var M=function(){if(h){return}h=new MutationObserver((function(){if(!y||y.size===0){return}var r=b();var n=__spreadArray([],y.values(),true);for(var t=0;t<n.length;t++){var i=n[t];i(r)}}));h.observe(document.documentElement,{attributeFilter:["dir"]})};var w=r("s",(function(r,n){M();y!==null&&y!==void 0?y:y=new Map;y.set(r,n)}));var A=r("k",(function(r){y.delete(r);if(y.size===0){h.disconnect();h=undefined;y=undefined}}));var O=r("j",(function(r,t){return!r||t==="img"||n.test(r)?r:"url(".concat(r,")")}));var p=r("u",(function(r,n,t){if(t===void 0){t="background"}if(!r){return undefined}if(n==="start"){var i="start-img--base";var a={"--ch-start-img--base":O(r.base,t)};if(r.active){a["--ch-start-img--active"]=O(r.active,t);i+=" start-img--active"}if(r.focus){a["--ch-start-img--focus"]=O(r.focus,t);i+=" start-img--focus"}if(r.hover){a["--ch-start-img--hover"]=O(r.hover,t);i+=" start-img--hover"}if(r.selected){a["--ch-start-img--selected"]=O(r.selected,t);i+=" start-img--selected"}if(r.disabled){a["--ch-start-img--disabled"]=O(r.disabled,t);i+=" start-img--disabled"}return{classes:i,styles:a}}var e="end-img--base";var u={"--ch-end-img--base":O(r.base,t)};if(r.active){u["--ch-end-img--active"]=O(r.active,t);e+=" end-img--active"}if(r.focus){u["--ch-end-img--focus"]=O(r.focus,t);e+=" end-img--focus"}if(r.hover){u["--ch-end-img--hover"]=O(r.hover,t);e+=" end-img--hover"}if(r.selected){u["--ch-end-img--selected"]=O(r.selected,t);e+=" end-img--selected"}if(r.disabled){u["--ch-end-img--disabled"]=O(r.disabled,t);e+=" end-img--disabled"}return{classes:e,styles:u}}))}}}));
//# sourceMappingURL=p-81126801.system.js.map