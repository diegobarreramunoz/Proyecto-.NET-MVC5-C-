<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.1"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:ms="urn:schemas-microsoft-com:xslt"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	exclude-result-prefixes="fo">
  <xsl:include href="page_style.xsl"/>
  <xsl:template name="pageFormat">
    <fo:simple-page-master master-name="simple"
        page-height="27.94cm"
        page-width="21.59cm"
        margin-top="1cm"
        margin-bottom="1cm"
        margin-left="1cm"
        margin-right="1cm">
      <fo:region-body reference-orientation="90"/>
    </fo:simple-page-master> 
  </xsl:template>

  <xsl:template name="page-simple-header">
    <fo:static-content flow-name="xsl-region-before" xsl:use-attribute-sets="body-Format">
    </fo:static-content>
  </xsl:template> 

  <xsl:template name="page-simple-footer">
    <fo:static-content flow-name="xsl-region-after" xsl:use-attribute-sets="body-Format">
    </fo:static-content>
  </xsl:template>
</xsl:stylesheet>