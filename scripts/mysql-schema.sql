-- Oficina Backend - MySQL schema blueprint (MySQL 8+)
-- Gera as tabelas necessárias para os módulos Cadastro, Estoque, Ordem de Serviço e Financeiro.

CREATE DATABASE IF NOT EXISTS oficina_db
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE oficina_db;

-- ============================================================================
-- Catálogo de Clientes (Cadastro)
-- ============================================================================

CREATE TABLE IF NOT EXISTS cad_clientes_origens (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Nome VARCHAR(120) NOT NULL,
    Descricao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_origens_nome (Nome)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo BIGINT NOT NULL,
    Nome VARCHAR(160) NOT NULL,
    NomeExibicao VARCHAR(160) NOT NULL,
    Documento VARCHAR(20) NOT NULL,
    Tipo INT NOT NULL,
    Status INT NOT NULL,
    Vip TINYINT(1) NOT NULL DEFAULT 0,
    Observacoes VARCHAR(500),
    OrigemCadastroId INT NOT NULL DEFAULT 0,
    Telefone VARCHAR(20),
    Email VARCHAR(160),
    Origem_Id BIGINT NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_documento (Documento),
    UNIQUE KEY UX_cad_clientes_codigo (Codigo),
    KEY IX_cad_clientes_origem (Origem_Id),
    CONSTRAINT FK_cad_clientes_origem FOREIGN KEY (Origem_Id)
        REFERENCES cad_clientes_origens (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_pf (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Cpf VARCHAR(14) NOT NULL,
    Rg VARCHAR(20),
    Data_Nascimento DATE,
    Genero VARCHAR(30),
    Estado_Civil VARCHAR(20),
    Profissao VARCHAR(120),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_pf_cliente (Cliente_Id),
    CONSTRAINT FK_cad_clientes_pf_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_pj (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Cnpj VARCHAR(20) NOT NULL,
    Razao_Social VARCHAR(180) NOT NULL,
    Nome_Fantasia VARCHAR(180),
    Inscricao_Estadual VARCHAR(30),
    Inscricao_Municipal VARCHAR(30),
    Responsavel VARCHAR(120),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_pj_cliente (Cliente_Id),
    UNIQUE KEY UX_cad_clientes_pj_cnpj (Cnpj),
    CONSTRAINT FK_cad_clientes_pj_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_enderecos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Tipo INT NOT NULL,
    Cep VARCHAR(12) NOT NULL,
    Logradouro VARCHAR(160) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Bairro VARCHAR(120) NOT NULL,
    Cidade VARCHAR(120) NOT NULL,
    Estado VARCHAR(60) NOT NULL,
    Pais VARCHAR(80),
    Complemento VARCHAR(120),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (Id),
    KEY IX_cad_clientes_enderecos_cliente_tipo_principal (Cliente_Id, Tipo, Principal),
    CONSTRAINT FK_cad_clientes_enderecos_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_contatos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Tipo INT NOT NULL,
    Valor VARCHAR(160) NOT NULL,
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_contatos_cliente_tipo_valor (Cliente_Id, Tipo, Valor),
    CONSTRAINT FK_cad_clientes_contatos_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_indicacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Indicador_Nome VARCHAR(160) NOT NULL,
    Indicador_Telefone VARCHAR(20),
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    KEY IX_cad_clientes_indicacoes_cliente (Cliente_Id),
    CONSTRAINT FK_cad_clientes_indicacoes_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_lgpd_consentimentos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Tipo INT NOT NULL,
    Aceito TINYINT(1) NOT NULL,
    Data DATETIME(6),
    Valido_Ate DATETIME(6),
    Observacoes VARCHAR(240),
    Canal VARCHAR(80) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_lgpd_consentimento_cliente (Cliente_Id),
    CONSTRAINT FK_cad_clientes_lgpd_consentimento_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_financeiro (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Limite_Credito DECIMAL(10,2),
    Prazo_Pagamento INT,
    Bloqueado TINYINT(1) NOT NULL DEFAULT 0,
    Observacoes VARCHAR(500),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_financeiro_cliente (Cliente_Id),
    CONSTRAINT FK_cad_clientes_financeiro_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_anexos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Nome VARCHAR(200) NOT NULL,
    Tipo VARCHAR(100) NOT NULL,
    Url VARCHAR(500) NOT NULL,
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_anexos_cliente_nome (Cliente_Id, Nome),
    CONSTRAINT FK_cad_clientes_anexos_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_clientes_documentos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Tipo VARCHAR(30) NOT NULL,
    Documento VARCHAR(40) NOT NULL,
    Data_Emissao DATE NULL,
    Data_Validade DATE NULL,
    Orgao_Expedidor VARCHAR(80),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_clientes_documentos_cliente_tipo_documento (Cliente_Id, Tipo, Documento),
    CONSTRAINT FK_cad_clientes_documentos_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- Colaboradores - Mecanicos
-- ============================================================================

CREATE TABLE IF NOT EXISTS cad_mecanicos_especialidades (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(16) NOT NULL,
    Nome VARCHAR(160) NOT NULL,
    Descricao VARCHAR(240),
    Ativo TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_especialidades_codigo (Codigo),
    UNIQUE KEY UX_cad_mecanicos_especialidades_nome (Nome)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(20) NOT NULL,
    Nome VARCHAR(120) NOT NULL,
    Sobrenome VARCHAR(120),
    Nome_Social VARCHAR(120),
    Documento_Principal VARCHAR(20) NOT NULL,
    Tipo_Documento INT NOT NULL DEFAULT 1,
    Data_Nascimento DATE,
    Data_Admissao DATE NOT NULL,
    Data_Demissao DATE,
    Status VARCHAR(20) NOT NULL DEFAULT 'Ativo',
    Especialidade_Principal_Id BIGINT NULL,
    Nivel VARCHAR(20) NOT NULL DEFAULT 'Pleno',
    Valor_Hora DECIMAL(10,2) NOT NULL DEFAULT 0,
    Carga_Horaria_Semanal INT NOT NULL DEFAULT 44,
    Observacoes VARCHAR(500),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_codigo (Codigo),
    UNIQUE KEY UX_cad_mecanicos_documento (Documento_Principal),
    KEY IX_cad_mecanicos_especialidade (Especialidade_Principal_Id),
    CONSTRAINT FK_cad_mecanicos_especialidade FOREIGN KEY (Especialidade_Principal_Id)
        REFERENCES cad_mecanicos_especialidades (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_especialidades_rel (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Especialidade_Id BIGINT NOT NULL,
    Nivel VARCHAR(20) NOT NULL DEFAULT 'Pleno',
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Anotacoes VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_especialidades_rel (Mecanico_Id, Especialidade_Id),
    KEY IX_cad_mecanicos_especialidades_rel_esp (Especialidade_Id),
    CONSTRAINT FK_cad_mecanicos_especialidades_rel_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_cad_mecanicos_especialidades_rel_especialidade FOREIGN KEY (Especialidade_Id)
        REFERENCES cad_mecanicos_especialidades (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_documentos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Tipo VARCHAR(30) NOT NULL,
    Numero VARCHAR(40) NOT NULL,
    Data_Emissao DATE,
    Data_Validade DATE,
    Orgao_Expedidor VARCHAR(80),
    Arquivo_Url VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_documentos_mecanico_numero (Mecanico_Id, Tipo, Numero),
    CONSTRAINT FK_cad_mecanicos_documentos_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_contatos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Tipo VARCHAR(20) NOT NULL,
    Valor VARCHAR(160) NOT NULL,
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_contatos_mecanico_tipo_valor (Mecanico_Id, Tipo, Valor),
    CONSTRAINT FK_cad_mecanicos_contatos_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_enderecos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Tipo VARCHAR(20) NOT NULL,
    Cep VARCHAR(12) NOT NULL,
    Logradouro VARCHAR(160) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Bairro VARCHAR(120) NOT NULL,
    Cidade VARCHAR(120) NOT NULL,
    Estado VARCHAR(60) NOT NULL,
    Pais VARCHAR(80),
    Complemento VARCHAR(120),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_enderecos_mecanico_tipo_principal (Mecanico_Id, Tipo, Principal),
    CONSTRAINT FK_cad_mecanicos_enderecos_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_certificacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Especialidade_Id BIGINT NULL,
    Titulo VARCHAR(160) NOT NULL,
    Instituicao VARCHAR(160),
    Data_Conclusao DATE,
    Data_Validade DATE,
    Codigo_Certificacao VARCHAR(60),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_certificacoes_mecanico_titulo (Mecanico_Id, Titulo),
    KEY IX_cad_mecanicos_certificacoes_especialidade (Especialidade_Id),
    CONSTRAINT FK_cad_mecanicos_certificacoes_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_cad_mecanicos_certificacoes_especialidade FOREIGN KEY (Especialidade_Id)
        REFERENCES cad_mecanicos_especialidades (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_disponibilidades (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Dia_Semana TINYINT NOT NULL,
    Hora_Inicio TIME NOT NULL,
    Hora_Fim TIME NOT NULL,
    Capacidade_Atendimentos INT NOT NULL DEFAULT 5,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_disponibilidades (Mecanico_Id, Dia_Semana, Hora_Inicio, Hora_Fim),
    CONSTRAINT FK_cad_mecanicos_disponibilidades_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_mecanicos_experiencias (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Mecanico_Id BIGINT NOT NULL,
    Empresa VARCHAR(160) NOT NULL,
    Cargo VARCHAR(120) NOT NULL,
    Data_Inicio DATE,
    Data_Fim DATE,
    Resumo_Atividades VARCHAR(400),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_mecanicos_experiencias (Mecanico_Id, Empresa, Cargo),
    KEY IX_cad_mecanicos_experiencias_mecanico (Mecanico_Id),
    CONSTRAINT FK_cad_mecanicos_experiencias_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;



-- ============================================================================
-- Catalogo de Fornecedores
-- ============================================================================

CREATE TABLE IF NOT EXISTS cad_fornecedores_segmentos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(20) NOT NULL,
    Nome VARCHAR(120) NOT NULL,
    Descricao VARCHAR(240),
    Ativo TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_segmentos_codigo (Codigo),
    UNIQUE KEY UX_cad_fornecedores_segmentos_nome (Nome)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(20) NOT NULL,
    Tipo VARCHAR(10) NOT NULL,
    Razao_Social VARCHAR(160) NOT NULL,
    Nome_Fantasia VARCHAR(160),
    Documento VARCHAR(20) NOT NULL,
    Inscricao_Estadual VARCHAR(30),
    Inscricao_Municipal VARCHAR(30),
    Segmento_Principal_Id BIGINT NULL,
    Website VARCHAR(160),
    Email VARCHAR(160),
    Telefone_Principal VARCHAR(30),
    Status VARCHAR(20) NOT NULL DEFAULT 'ATIVO',
    Condicao_Pagamento_Padrao VARCHAR(120),
    Prazo_Entrega_Medio INT NULL,
    Nota_Media DECIMAL(4,2) NULL,
    Observacoes VARCHAR(600),
    Prazo_Garantia_Padrao VARCHAR(120),
    Termos_Negociados VARCHAR(240),
    Atendimento_Personalizado TINYINT(1) NOT NULL DEFAULT 0,
    Retirada_Local TINYINT(1) NOT NULL DEFAULT 0,
    Rating_Logistica DECIMAL(4,2) NULL,
    Rating_Qualidade DECIMAL(4,2) NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_codigo (Codigo),
    UNIQUE KEY UX_cad_fornecedores_documento (Documento),
    KEY IX_cad_fornecedores_segmento (Segmento_Principal_Id),
    CONSTRAINT FK_cad_fornecedores_segmento FOREIGN KEY (Segmento_Principal_Id)
        REFERENCES cad_fornecedores_segmentos (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_segmentos_rel (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Segmento_Id BIGINT NOT NULL,
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Observacoes VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_segmentos_rel (Fornecedor_Id, Segmento_Id),
    KEY IX_cad_fornecedores_segmentos_rel_segmento (Segmento_Id),
    CONSTRAINT FK_cad_fornecedores_segmentos_rel_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_cad_fornecedores_segmentos_rel_segmento FOREIGN KEY (Segmento_Id)
        REFERENCES cad_fornecedores_segmentos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_enderecos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Tipo VARCHAR(30) NOT NULL,
    Cep VARCHAR(12),
    Logradouro VARCHAR(160) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Bairro VARCHAR(120) NOT NULL,
    Cidade VARCHAR(120) NOT NULL,
    Estado VARCHAR(60) NOT NULL,
    Pais VARCHAR(80),
    Complemento VARCHAR(120),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    KEY IX_cad_fornecedores_enderecos_fornecedor_tipo (Fornecedor_Id, Tipo),
    CONSTRAINT FK_cad_fornecedores_enderecos_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_contatos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Tipo VARCHAR(30) NOT NULL,
    Valor VARCHAR(160) NOT NULL,
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_contatos (Fornecedor_Id, Tipo, Valor),
    CONSTRAINT FK_cad_fornecedores_contatos_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_representantes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Nome VARCHAR(120) NOT NULL,
    Cargo VARCHAR(120),
    Email VARCHAR(160),
    Telefone VARCHAR(30),
    Celular VARCHAR(30),
    Preferencia_Contato VARCHAR(30),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    Observacoes VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_representantes (Fornecedor_Id, Nome),
    CONSTRAINT FK_cad_fornecedores_representantes_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_bancos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Banco VARCHAR(80) NOT NULL,
    Agencia VARCHAR(20),
    Conta VARCHAR(20),
    Digito VARCHAR(5),
    Tipo_Conta VARCHAR(20),
    Pix_Chave VARCHAR(120),
    Observacoes VARCHAR(240),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_bancos (Fornecedor_Id, Banco, Agencia, Conta, Digito),
    CONSTRAINT FK_cad_fornecedores_bancos_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_documentos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Tipo VARCHAR(60) NOT NULL,
    Numero VARCHAR(60) NOT NULL,
    Data_Emissao DATE,
    Data_Validade DATE,
    Orgao_Expedidor VARCHAR(120),
    Arquivo_Url VARCHAR(240),
    Observacoes VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_documentos (Fornecedor_Id, Tipo, Numero),
    CONSTRAINT FK_cad_fornecedores_documentos_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_certificacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Titulo VARCHAR(160) NOT NULL,
    Instituicao VARCHAR(160),
    Data_Emissao DATE,
    Data_Validade DATE,
    Codigo_Certificacao VARCHAR(60),
    Escopo VARCHAR(200),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_certificacoes (Fornecedor_Id, Titulo),
    CONSTRAINT FK_cad_fornecedores_certificacoes_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_fornecedores_avaliacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Fornecedor_Id BIGINT NOT NULL,
    Data_Avaliacao DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Avaliado_Por VARCHAR(120),
    Categoria VARCHAR(60),
    Nota DECIMAL(4,2) NOT NULL,
    Comentarios VARCHAR(400),
    PRIMARY KEY (Id),
    KEY IX_cad_fornecedores_avaliacoes_fornecedor (Fornecedor_Id, Data_Avaliacao),
    CONSTRAINT FK_cad_fornecedores_avaliacoes_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_veiculos_marcas (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Nome VARCHAR(120) NOT NULL,
    Pais VARCHAR(80),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_veiculos_marcas_nome (Nome)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_veiculos_modelos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Marca_Id BIGINT NOT NULL,
    Nome VARCHAR(160) NOT NULL,
    Ano_Inicio INT,
    Ano_Fim INT,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_veiculos_modelos_marca_nome (Marca_Id, Nome),
    CONSTRAINT FK_cad_veiculos_modelos_marca FOREIGN KEY (Marca_Id)
        REFERENCES cad_veiculos_marcas (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS cad_veiculos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Placa VARCHAR(10) NOT NULL,
    Marca VARCHAR(80),
    Modelo_Id BIGINT NULL,
    Ano INT,
    Cor VARCHAR(40),
    Chassi VARCHAR(40),
    Renavam VARCHAR(20),
    Combustivel VARCHAR(40),
    Observacao VARCHAR(240),
    Principal TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_veiculos_placa (Placa),
    UNIQUE KEY UX_cad_veiculos_renavam (Renavam),
    KEY IX_cad_veiculos_modelo (Modelo_Id),
    CONSTRAINT FK_cad_veiculos_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_cad_veiculos_modelo FOREIGN KEY (Modelo_Id)
        REFERENCES cad_veiculos_modelos (Id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- Estoque
-- ============================================================================

-- ============================================================================
-- Estoque e Catalogo de Pecas
-- ============================================================================

CREATE TABLE IF NOT EXISTS est_pecas_categorias (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(30) NOT NULL,
    Nome VARCHAR(160) NOT NULL,
    Categoria_Pai_Id BIGINT NULL,
    Descricao VARCHAR(240),
    Ativo TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_categorias_codigo (Codigo),
    KEY IX_est_pecas_categorias_pai (Categoria_Pai_Id),
    CONSTRAINT FK_est_pecas_categorias_pai FOREIGN KEY (Categoria_Pai_Id)
        REFERENCES est_pecas_categorias (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas_unidades (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Sigla VARCHAR(10) NOT NULL,
    Descricao VARCHAR(80) NOT NULL,
    Observacao VARCHAR(120),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_unidades_sigla (Sigla)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas_marcas (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Nome VARCHAR(160) NOT NULL,
    Pais VARCHAR(80),
    Website VARCHAR(160),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_marcas_nome (Nome)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(40) NOT NULL,
    Descricao VARCHAR(200) NOT NULL,
    Descricao_Detalhada VARCHAR(600),
    Categoria_Id BIGINT NULL,
    Unidade_Id BIGINT NULL,
    Marca_Id BIGINT NULL,
    Fornecedor_Id BIGINT NULL,
    Numero_Fabricante VARCHAR(80),
    Codigo_Barras VARCHAR(64),
    Ncm VARCHAR(10),
    Unidade_Compra VARCHAR(10),
    Preco_Custo DECIMAL(18,2) NOT NULL DEFAULT 0,
    Preco_Unitario DECIMAL(18,2) NOT NULL DEFAULT 0,
    Preco_Minimo DECIMAL(18,2),
    Preco_Maximo DECIMAL(18,2),
    Margem_Sugerida DECIMAL(5,2),
    Prazo_Reposicao INT,
    Quantidade INT NOT NULL DEFAULT 0,
    Estoque_Minimo INT NOT NULL DEFAULT 0,
    Estoque_Maximo INT,
    Localizacao_Estoque VARCHAR(60),
    Peso_Gramas DECIMAL(10,2),
    Altura_CM DECIMAL(10,2),
    Largura_CM DECIMAL(10,2),
    Comprimento_CM DECIMAL(10,2),
    Observacoes VARCHAR(400),
    Ativo TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_codigo (Codigo),
    UNIQUE KEY UX_est_pecas_codigo_barras (Codigo_Barras),
    KEY IX_est_pecas_categoria (Categoria_Id),
    KEY IX_est_pecas_unidade (Unidade_Id),
    KEY IX_est_pecas_marca (Marca_Id),
    KEY IX_est_pecas_fornecedor (Fornecedor_Id),
    CONSTRAINT FK_est_pecas_categoria FOREIGN KEY (Categoria_Id)
        REFERENCES est_pecas_categorias (Id)
        ON DELETE SET NULL,
    CONSTRAINT FK_est_pecas_unidade FOREIGN KEY (Unidade_Id)
        REFERENCES est_pecas_unidades (Id)
        ON DELETE SET NULL,
    CONSTRAINT FK_est_pecas_marca FOREIGN KEY (Marca_Id)
        REFERENCES est_pecas_marcas (Id)
        ON DELETE SET NULL,
    CONSTRAINT FK_est_pecas_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas_fornecedores (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Peca_Id BIGINT NOT NULL,
    Fornecedor_Id BIGINT NOT NULL,
    Codigo_Fornecedor VARCHAR(60),
    Prazo_Entrega_Dias INT,
    Preco_Custo DECIMAL(18,2) NOT NULL DEFAULT 0,
    Moeda VARCHAR(10) NOT NULL DEFAULT 'BRL',
    Lote_Minimo INT,
    Condicao_Pagamento VARCHAR(120),
    Status VARCHAR(20) NOT NULL DEFAULT 'ATIVO',
    Observacoes VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_fornecedores (Peca_Id, Fornecedor_Id),
    KEY IX_est_pecas_fornecedores_fornecedor (Fornecedor_Id),
    CONSTRAINT FK_est_pecas_fornecedores_peca FOREIGN KEY (Peca_Id)
        REFERENCES est_pecas (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_est_pecas_fornecedores_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas_aplicacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Peca_Id BIGINT NOT NULL,
    Modelo_Veiculo_Id BIGINT NOT NULL,
    Ano_Inicio INT,
    Ano_Fim INT,
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_aplicacoes (Peca_Id, Modelo_Veiculo_Id, Ano_Inicio, Ano_Fim),
    KEY IX_est_pecas_aplicacoes_modelo (Modelo_Veiculo_Id),
    CONSTRAINT FK_est_pecas_aplicacoes_peca FOREIGN KEY (Peca_Id)
        REFERENCES est_pecas (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_est_pecas_aplicacoes_modelo FOREIGN KEY (Modelo_Veiculo_Id)
        REFERENCES cad_veiculos_modelos (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas_precos_historico (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Peca_Id BIGINT NOT NULL,
    Data_Referencia DATE NOT NULL,
    Preco_Custo DECIMAL(18,2) NOT NULL DEFAULT 0,
    Preco_Venda DECIMAL(18,2) NOT NULL DEFAULT 0,
    Moeda VARCHAR(10) NOT NULL DEFAULT 'BRL',
    Fonte VARCHAR(120),
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_precos_historico (Peca_Id, Data_Referencia),
    CONSTRAINT FK_est_pecas_precos_historico_peca FOREIGN KEY (Peca_Id)
        REFERENCES est_pecas (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_pecas_arquivos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Peca_Id BIGINT NOT NULL,
    Tipo VARCHAR(40) NOT NULL,
    Nome_Arquivo VARCHAR(160) NOT NULL,
    Url VARCHAR(240),
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    KEY IX_est_pecas_arquivos_peca_tipo (Peca_Id, Tipo),
    CONSTRAINT FK_est_pecas_arquivos_peca FOREIGN KEY (Peca_Id)
        REFERENCES est_pecas (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_movimentacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Peca_Id BIGINT NOT NULL,
    Quantidade INT NOT NULL,
    Tipo VARCHAR(10) NOT NULL,
    Referencia VARCHAR(160),
    Quantidade_Anterior INT,
    Quantidade_Posterior INT,
    Valor_Unitario DECIMAL(18,2),
    Valor_Total DECIMAL(18,2),
    Documento VARCHAR(60),
    Origem VARCHAR(60),
    Observacao VARCHAR(240),
    PRIMARY KEY (Id),
    KEY IX_est_movimentacoes_peca (Peca_Id),
    CONSTRAINT FK_est_movimentacoes_peca FOREIGN KEY (Peca_Id)
        REFERENCES est_pecas (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- Ordem de Serviço
-- ============================================================================

CREATE TABLE IF NOT EXISTS os_ordens (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Cliente_Id BIGINT NOT NULL,
    Mecanico_Id BIGINT NOT NULL,
    Descricao_Problema VARCHAR(400) NOT NULL,
    Status VARCHAR(20) NOT NULL,
    Data_Abertura DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Data_Conclusao DATETIME(6),
    PRIMARY KEY (Id),
    KEY IX_os_ordens_cliente (Cliente_Id),
    KEY IX_os_ordens_mecanico (Mecanico_Id),
    CONSTRAINT FK_os_ordens_cliente FOREIGN KEY (Cliente_Id)
        REFERENCES cad_clientes (Id)
        ON DELETE RESTRICT,
    CONSTRAINT FK_os_ordens_mecanico FOREIGN KEY (Mecanico_Id)
        REFERENCES cad_mecanicos (Id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS os_itens (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Ordem_Servico_Id BIGINT NOT NULL,
    Peca_Id BIGINT NULL,
    Descricao VARCHAR(200) NOT NULL,
    Quantidade INT NOT NULL,
    Valor_Unitario DECIMAL(18,2) NOT NULL DEFAULT 0,
    PRIMARY KEY (Id),
    KEY IX_os_itens_ordem (Ordem_Servico_Id),
    KEY IX_os_itens_peca (Peca_Id),
    CONSTRAINT FK_os_itens_ordem FOREIGN KEY (Ordem_Servico_Id)
        REFERENCES os_ordens (Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_os_itens_peca FOREIGN KEY (Peca_Id)
        REFERENCES est_pecas (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- Financeiro
-- ============================================================================

CREATE TABLE IF NOT EXISTS fin_pagamentos (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Ordem_Servico_Id BIGINT NOT NULL,
    Meio VARCHAR(12) NOT NULL,
    Valor DECIMAL(18,2) NOT NULL,
    Status VARCHAR(12) NOT NULL,
    Transacao_Id VARCHAR(64),
    PRIMARY KEY (Id),
    KEY IX_fin_pagamentos_ordem (Ordem_Servico_Id),
    CONSTRAINT FK_fin_pagamentos_ordem FOREIGN KEY (Ordem_Servico_Id)
        REFERENCES os_ordens (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS fin_nfes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Ordem_Servico_Id BIGINT NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Chave_Acesso VARCHAR(60) NOT NULL,
    Status VARCHAR(12) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_fin_nfes_numero (Numero),
    KEY IX_fin_nfes_ordem (Ordem_Servico_Id),
    CONSTRAINT FK_fin_nfes_ordem FOREIGN KEY (Ordem_Servico_Id)
        REFERENCES os_ordens (Id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- Dados auxiliares
-- ============================================================================

-- View simples para facilitar consultas de clientes resumidos.
CREATE OR REPLACE VIEW vw_cad_clientes_resumo AS
SELECT
    c.Id,
    c.Codigo,
    c.Nome,
    c.NomeExibicao,
    c.Documento,
    c.Tipo,
    c.Status,
    c.Vip,
    o.Nome AS Origem_Nome,
    o.Descricao AS Origem_Descricao,
    c.Created_At,
    c.Updated_At
FROM cad_clientes c
LEFT JOIN cad_clientes_origens o ON o.Id = c.Origem_Id;

-- Fim do script.
