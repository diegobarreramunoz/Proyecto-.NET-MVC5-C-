<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.1"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:ms="urn:schemas-microsoft-com:xslt"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	exclude-result-prefixes="fo">
  <!-- REFERENCIAS -->
  <xsl:include href="page_style.xsl" />
  <xsl:include href="page_setup.xsl"/>
  <xsl:include href="page_descripcion.xsl"/> 

  <xsl:template match="ComprobanteDescanso">
    <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
      <fo:layout-master-set>
        <xsl:call-template name="pageFormat" />
      </fo:layout-master-set>
      
      <fo:page-sequence master-reference="simple">         
        <fo:flow flow-name="xsl-region-body" xsl:use-attribute-sets="body-Format">
          <!-- [ copia 1 ] -->
          <xsl:call-template name="page-descripcion">
            <xsl:with-param name="nomCopia" select = "'TRABAJADOR'" />
          </xsl:call-template>
          <fo:block  >- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -</fo:block>
          <!-- [ copia 2 ] -->
          <xsl:call-template name="page-descripcion">
            <xsl:with-param name="nomCopia" select = "'TIENDA'" />
          </xsl:call-template>
          <fo:block  >- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -</fo:block>
          <!-- [ copia 3 ] -->
          <xsl:call-template name="page-descripcion">
            <xsl:with-param name="nomCopia" select = "'RECURSOS HUMANOS'" />
          </xsl:call-template>
          <!-- [ pagina 1 ] -->          
        </fo:flow>
      </fo:page-sequence>
      
    </fo:root>
  </xsl:template>
</xsl:stylesheet>