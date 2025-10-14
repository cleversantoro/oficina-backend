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



CREATE TABLE IF NOT EXISTS cad_fornecedores (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Razao_Social VARCHAR(160) NOT NULL,
    Cnpj VARCHAR(20) NOT NULL,
    Contato VARCHAR(160) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_cad_fornecedores_cnpj (Cnpj)
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

CREATE TABLE IF NOT EXISTS est_pecas (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Codigo VARCHAR(40) NOT NULL,
    Descricao VARCHAR(200) NOT NULL,
    Preco_Unitario DECIMAL(18,2) NOT NULL DEFAULT 0,
    Quantidade INT NOT NULL DEFAULT 0,
    Fornecedor_Id BIGINT NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_est_pecas_codigo (Codigo),
    KEY IX_est_pecas_fornecedor (Fornecedor_Id),
    CONSTRAINT FK_est_pecas_fornecedor FOREIGN KEY (Fornecedor_Id)
        REFERENCES cad_fornecedores (Id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS est_movimentacoes (
    Id BIGINT NOT NULL AUTO_INCREMENT,
    Created_At DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    Updated_At DATETIME(6) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(6),
    Peca_Id BIGINT NOT NULL,
    Quantidade INT NOT NULL,
    Tipo VARCHAR(10) NOT NULL,
    Referencia VARCHAR(160),
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
