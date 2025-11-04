USE oficina_db;

-- ============================================================================
-- Seeds para cadastro de fornecedores
-- ============================================================================

INSERT INTO cad_fornecedores_segmentos (Codigo, Nome, Descricao, Ativo)
VALUES
    ('AUTOPE', 'Autopecas', 'Fabricantes e distribuidores de pecas mecanicas', 1),
    ('LUBRI', 'Lubrificantes', 'Provedores de oleos e fluidos automotivos', 1),
    ('ELETR', 'Eletrica e Eletronica', 'Componentes de ignicao e modulos eletricos', 1),
    ('CLIMA', 'Climatizacao', 'Sistemas de ar condicionado e ventilacao', 1),
    ('SUSP', 'Suspensao e Freios', 'Componentes de seguranca e suspensao', 1)
ON DUPLICATE KEY UPDATE
    Nome = VALUES(Nome),
    Descricao = VALUES(Descricao),
    Ativo = VALUES(Ativo),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores (
    Codigo, Tipo, Razao_Social, Nome_Fantasia, Documento, Inscricao_Estadual,
    Inscricao_Municipal, Segmento_Principal_Id, Website, Email, Telefone_Principal,
    Status, Condicao_Pagamento_Padrao, Prazo_Entrega_Medio, Nota_Media, Observacoes,
    Prazo_Garantia_Padrao, Termos_Negociados, Atendimento_Personalizado, Retirada_Local,
    Rating_Logistica, Rating_Qualidade
)
VALUES
    (
        'FORN-0001',
        'PJ',
        'Distribuidora Autosul LTDA',
        'Autosul',
        '00987654321098',
        '112223334445',
        '123456789',
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'AUTOPE'),
        'https://www.autosul.com.br',
        'comercial@autosul.com.br',
        '(11) 4002-8922',
        'ATIVO',
        'Boleto 28 dias',
        5,
        4.60,
        'Linha premium de filtros, lubrificantes e componentes do motor.',
        'Garantia de 6 meses para itens mecanicos conforme fabricante.',
        'Desconto adicional de 3% para pagamento antecipado.',
        1,
        1,
        4.70,
        4.80
    ),
    (
        'FORN-0002',
        'PJ',
        'Pecas Premium SA',
        'Premium Parts',
        '00112233445566',
        '998877665544',
        '445566778',
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'SUSP'),
        'https://fornecedor-premium.example.com',
        'vendas@pecaspremium.com',
        '(11) 3254-7788',
        'ATIVO',
        'Pix imediato com 5% off ou boleto 21 dias',
        4,
        4.50,
        'Especialista em freios e suspensao de alta performance.',
        'Garantia de 12 meses para itens homologados.',
        'Entrega consignada mediante contrato e previsao de giro.',
        1,
        0,
        4.60,
        4.75
    ),
    (
        'FORN-0003',
        'PJ',
        'Logparts Comercio e Logistica LTDA',
        'Logparts',
        '00334455667788',
        '221133445566',
        '556677889',
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'ELETR'),
        'https://www.logparts.com.br',
        'suporte@logparts.com.br',
        '(11) 2555-9900',
        'ATIVO',
        'Faturamento quinzenal com limite de credito ativo.',
        3,
        4.40,
        'Distribuidor nacional com centro de distribuicao em SP e PR.',
        'Garantia de 1 ano para modulos e componentes eletricos.',
        'Frete incluso em pedidos acima de R$ 2.000,00.',
        1,
        1,
        4.55,
        4.60
    ),
    (
        'FORN-0004',
        'PJ',
        'Motores Brasil Comercial LTDA',
        'Motores Brasil',
        '00778899001122',
        '445566771122',
        '112244335',
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'AUTOPE'),
        'https://www.motoresbr.com',
        'atendimento@motoresbr.com',
        '(11) 3090-4020',
        'ATIVO',
        'Boleto 30 dias com limite sob consulta.',
        7,
        4.30,
        'Especialista em componentes pesados e kits de reparo.',
        'Garantia ate 18 meses conforme linha premium.',
        'Oferece consignacao para linhas de alta rotatividade.',
        0,
        1,
        4.30,
        4.55
    ),
    (
        'FORN-0005',
        'PJ',
        'AutoClima Servicos Integrados LTDA',
        'AutoClima',
        '00556677889900',
        '556677889900',
        '778899001',
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'CLIMA'),
        'https://www.autoclima.com.br',
        'contato@autoclima.com.br',
        '(11) 2789-4545',
        'ATIVO',
        'Pagamento em 21 dias com desconto progressivo.',
        6,
        4.20,
        'Foco em sistemas de ar condicionado, evaporadores e compressores.',
        'Garantia de 1 ano nos compressores instalados por rede credenciada.',
        'Contrato de SLA para entregas urgentes em ate 12h em SP.',
        1,
        0,
        4.40,
        4.65
    )
ON DUPLICATE KEY UPDATE
    Tipo = VALUES(Tipo),
    Razao_Social = VALUES(Razao_Social),
    Nome_Fantasia = VALUES(Nome_Fantasia),
    Inscricao_Estadual = VALUES(Inscricao_Estadual),
    Inscricao_Municipal = VALUES(Inscricao_Municipal),
    Segmento_Principal_Id = VALUES(Segmento_Principal_Id),
    Website = VALUES(Website),
    Email = VALUES(Email),
    Telefone_Principal = VALUES(Telefone_Principal),
    Status = VALUES(Status),
    Condicao_Pagamento_Padrao = VALUES(Condicao_Pagamento_Padrao),
    Prazo_Entrega_Medio = VALUES(Prazo_Entrega_Medio),
    Nota_Media = VALUES(Nota_Media),
    Observacoes = VALUES(Observacoes),
    Prazo_Garantia_Padrao = VALUES(Prazo_Garantia_Padrao),
    Termos_Negociados = VALUES(Termos_Negociados),
    Atendimento_Personalizado = VALUES(Atendimento_Personalizado),
    Retirada_Local = VALUES(Retirada_Local),
    Rating_Logistica = VALUES(Rating_Logistica),
    Rating_Qualidade = VALUES(Rating_Qualidade),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_segmentos_rel (Fornecedor_Id, Segmento_Id, Principal, Observacoes)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'AUTOPE'),
        1,
        'Distribuicao nacional de filtros e itens do powertrain.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'LUBRI'),
        0,
        'Revenda autorizada de lubrificantes premium.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'SUSP'),
        1,
        'Portifolio de amortecedores e sistemas de freio de alta performance.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0003'),
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'ELETR'),
        1,
        'Distribuidor autorizado de modulos, sensores e ignicao.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0004'),
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'AUTOPE'),
        1,
        'Especializado em componentes de motores e kits de revisao.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'CLIMA'),
        1,
        'Cobertura especializada para ar condicionado automotivo.'
    )
ON DUPLICATE KEY UPDATE
    Principal = VALUES(Principal),
    Observacoes = VALUES(Observacoes),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_enderecos (
    Fornecedor_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais,
    Complemento, Principal, Observacao
)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'Matriz',
        '04567-020',
        'Av. Engenheiro Luis Carlos Berrini',
        '1200',
        'Brooklin',
        'Sao Paulo',
        'SP',
        'Brasil',
        'Torre B, 17 andar',
        1,
        'Centro de distribuicao nacional.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'Matriz',
        '06455-000',
        'Alameda Araguaia',
        '2050',
        'Tambore',
        'Barueri',
        'SP',
        'Brasil',
        NULL,
        1,
        'Armazem climatizado para freios premium.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0003'),
        'Matriz',
        '09960-580',
        'Rua da Industria',
        '880',
        'Centro',
        'Diadema',
        'SP',
        'Brasil',
        'Galpao 3',
        1,
        'Cross-docking com monitoramento 24h.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        'Filial',
        '03045-000',
        'Rua do Thermo',
        '455',
        'Mooca',
        'Sao Paulo',
        'SP',
        'Brasil',
        'Centro tecnico',
        0,
        'Laboratorio para testes de climatizacao.'
    )
ON DUPLICATE KEY UPDATE
    Logradouro = VALUES(Logradouro),
    Numero = VALUES(Numero),
    Bairro = VALUES(Bairro),
    Cidade = VALUES(Cidade),
    Estado = VALUES(Estado),
    Pais = VALUES(Pais),
    Complemento = VALUES(Complemento),
    Principal = VALUES(Principal),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_contatos (Fornecedor_Id, Tipo, Valor, Principal, Observacao)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'Comercial',
        'comercial@autosul.com.br',
        1,
        'Atendimento de segunda a sexta das 8h as 18h.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'Suporte',
        '(11) 0800-020-9090',
        0,
        'Canal exclusivo para reclamacoes e devolucoes.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'Comercial',
        '(11) 3254-7788',
        1,
        'Equipe comercial linha premium.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0003'),
        'Tecnico',
        'engenharia@logparts.com.br',
        0,
        'Interacao tecnica e homologacoes.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        'Emergencial',
        '(11) 9 9988-6600',
        1,
        'Plantao para suporte de climatizacao.'
    )
ON DUPLICATE KEY UPDATE
    Principal = VALUES(Principal),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_representantes (
    Fornecedor_Id, Nome, Cargo, Email, Telefone, Celular, Preferencia_Contato, Principal, Observacoes
)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'Renata Alves',
        'Executiva de Contas Senior',
        'renata.alves@autosul.com.br',
        '(11) 4002-8922',
        '(11) 9 9000-1100',
        'E-mail',
        1,
        'Responsavel por negociacoes corporativas.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'Joao Batista',
        'Gerente Regional',
        'joao.batista@pecaspremium.com',
        NULL,
        '(11) 9 9777-2222',
        'Telefone',
        1,
        'Visitas tecnicas trimestrais.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0003'),
        'Paulo Siqueira',
        'Consultor de Negocios',
        'paulo.siqueira@logparts.com.br',
        '(11) 2555-9900',
        '(11) 9 9555-4400',
        'WhatsApp',
        1,
        'Contato direto para atualizacao de tabelas.'
    )
ON DUPLICATE KEY UPDATE
    Cargo = VALUES(Cargo),
    Email = VALUES(Email),
    Telefone = VALUES(Telefone),
    Celular = VALUES(Celular),
    Preferencia_Contato = VALUES(Preferencia_Contato),
    Principal = VALUES(Principal),
    Observacoes = VALUES(Observacoes),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_bancos (
    Fornecedor_Id, Banco, Agencia, Conta, Digito, Tipo_Conta, Pix_Chave, Observacoes, Principal
)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'Itau',
        '1234',
        '56789',
        '0',
        'Corrente',
        'financeiro@autosul.com.br',
        'Preferir TED em horario comercial.',
        1
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'Bradesco',
        '2299',
        '110022',
        '5',
        'Corrente',
        '55112233445566',
        'Informar numero do pedido no comprovante.',
        1
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        'Santander',
        '0333',
        '778899',
        '1',
        'Corrente',
        'autoclima@pix.com.br',
        'Contrato prevendo desconto de antecipacao em ate 2 dias uteis.',
        1
    )
ON DUPLICATE KEY UPDATE
    Tipo_Conta = VALUES(Tipo_Conta),
    Pix_Chave = VALUES(Pix_Chave),
    Observacoes = VALUES(Observacoes),
    Principal = VALUES(Principal),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_documentos (
    Fornecedor_Id, Tipo, Numero, Data_Emissao, Data_Validade, Orgao_Expedidor, Arquivo_Url, Observacoes
)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'Certidao Negativa Federal',
        'CNF-2024-1098',
        '2024-01-12',
        '2025-01-11',
        'Receita Federal',
        'https://docs.autosul.com.br/certidoes/cnf-2024-1098.pdf',
        'Valida ate janeiro de 2025.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0003'),
        'Certidao FGTS',
        'FGTS-2024-5544',
        '2024-03-01',
        '2024-09-01',
        'Caixa Economica Federal',
        'https://www.logparts.com.br/docs/fgts-2024-5544.pdf',
        'Solicitar nova via apos setembro.'
    )
ON DUPLICATE KEY UPDATE
    Data_Emissao = VALUES(Data_Emissao),
    Data_Validade = VALUES(Data_Validade),
    Orgao_Expedidor = VALUES(Orgao_Expedidor),
    Arquivo_Url = VALUES(Arquivo_Url),
    Observacoes = VALUES(Observacoes),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_certificacoes (
    Fornecedor_Id, Titulo, Instituicao, Data_Emissao, Data_Validade, Codigo_Certificacao, Escopo
)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'ISO 9001:2015',
        'Bureau Veritas',
        '2023-07-15',
        '2026-07-14',
        'ISO9001-4552',
        'Gestao da qualidade para distribuicao de componentes automotivos.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        'Certificacao Inmetro Compressores',
        'Inmetro',
        '2022-10-10',
        '2025-10-09',
        'INM-AC-2022',
        'Compressores e evaporadores certificados.'
    )
ON DUPLICATE KEY UPDATE
    Instituicao = VALUES(Instituicao),
    Data_Emissao = VALUES(Data_Emissao),
    Data_Validade = VALUES(Data_Validade),
    Codigo_Certificacao = VALUES(Codigo_Certificacao),
    Escopo = VALUES(Escopo),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_fornecedores_avaliacoes (
    Fornecedor_Id, Data_Avaliacao, Avaliado_Por, Categoria, Nota, Comentarios
)
VALUES
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        CURRENT_TIMESTAMP(6) - INTERVAL 30 DAY,
        'Equipe de Compras',
        'Qualidade',
        4.80,
        'Itens entregues dentro dos padroes e sem divergencias.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        CURRENT_TIMESTAMP(6) - INTERVAL 15 DAY,
        'Coordenacao Tecnica',
        'Suporte Tecnico',
        4.70,
        'Assistencia eficiente em homologacao de pastilhas ceramicas.'
    ),
    (
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        CURRENT_TIMESTAMP(6) - INTERVAL 7 DAY,
        'Equipe de Operacoes',
        'Logistica',
        4.50,
        'Cumpriu SLA de entrega emergencial para compressores.'
    )
ON DUPLICATE KEY UPDATE
    Avaliado_Por = VALUES(Avaliado_Por),
    Categoria = VALUES(Categoria),
    Nota = VALUES(Nota),
    Comentarios = VALUES(Comentarios),
    Updated_At = CURRENT_TIMESTAMP(6);

-- ============================================================================
-- Seeds para catalogo de pecas
-- ============================================================================

INSERT INTO est_pecas_categorias (Codigo, Nome, Descricao, Ativo)
VALUES
    ('CAT-ENG', 'Componentes de Motor', 'Itens aplicados ao grupo motriz', 1),
    ('CAT-FRE', 'Sistema de Freio', 'Pastilhas, discos e hidraulico', 1),
    ('CAT-CLM', 'Climatizacao', 'Componentes de ar condicionado e ventilacao', 1)
ON DUPLICATE KEY UPDATE
    Nome = VALUES(Nome),
    Descricao = VALUES(Descricao),
    Ativo = VALUES(Ativo),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_categorias (Codigo, Nome, Categoria_Pai_Id, Descricao, Ativo)
VALUES
    (
        'CAT-FILTROS',
        'Filtros e Elementos',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-ENG'),
        'Filtros de oleo, ar e combustivel',
        1
    ),
    (
        'CAT-IGN',
        'Ignicao',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-ENG'),
        'Velas, bobinas e cabos de velas',
        1
    ),
    (
        'CAT-COMP',
        'Compressores e HVAC',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-CLM'),
        'Compressores, evaporadores e filtros secadores',
        1
    )
ON DUPLICATE KEY UPDATE
    Categoria_Pai_Id = VALUES(Categoria_Pai_Id),
    Descricao = VALUES(Descricao),
    Ativo = VALUES(Ativo),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_unidades (Sigla, Descricao, Observacao)
VALUES
    ('UN', 'Unidade', 'Unidade individual'),
    ('CX', 'Caixa', 'Embalagem com multiplas unidades'),
    ('KIT', 'Kit', 'Conjunto com componentes diversos')
ON DUPLICATE KEY UPDATE
    Descricao = VALUES(Descricao),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_marcas (Nome, Pais, Website)
VALUES
    ('Mahle', 'Alemanha', 'https://www.mahle-aftermarket.com'),
    ('Bosch', 'Alemanha', 'https://www.boschautoparts.com'),
    ('NGK', 'Japao', 'https://www.ngkntk.com'),
    ('Denso', 'Japao', 'https://www.denso.com')
ON DUPLICATE KEY UPDATE
    Pais = VALUES(Pais),
    Website = VALUES(Website),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas (
    Codigo, Descricao, Descricao_Detalhada, Categoria_Id, Unidade_Id, Marca_Id, Fornecedor_Id,
    Numero_Fabricante, Codigo_Barras, Ncm, Unidade_Compra, Preco_Custo, Preco_Unitario,
    Preco_Minimo, Preco_Maximo, Margem_Sugerida, Prazo_Reposicao, Quantidade, Estoque_Minimo,
    Estoque_Maximo, Localizacao_Estoque, Peso_Gramas, Altura_CM, Largura_CM, Comprimento_CM,
    Observacoes, Ativo
)
VALUES
    (
        'FLT-001',
        'Filtro de oleo sintetico',
        'Filtro linha leve com elemento em celulose reforcada e valvula anti-retorno.',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-FILTROS'),
        (SELECT Id FROM est_pecas_unidades WHERE Sigla = 'UN'),
        (SELECT Id FROM est_pecas_marcas WHERE Nome = 'Mahle'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'OC-90',
        '7891234560011',
        '84212390',
        'UN',
        24.50,
        39.90,
        35.00,
        49.90,
        35.00,
        7,
        120,
        30,
        250,
        'A01-B2',
        420,
        9.50,
        8.00,
        8.00,
        'Aplicacao em motores 1.0 a 2.0 flex.',
        1
    ),
    (
        'PST-020',
        'Pastilha de freio dianteira ceramica',
        'Pastilha ceramica para uso severo com sensor de desgaste integrado.',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-FRE'),
        (SELECT Id FROM est_pecas_unidades WHERE Sigla = 'KIT'),
        (SELECT Id FROM est_pecas_marcas WHERE Nome = 'Bosch'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'BPX-200',
        '7891234560202',
        '87083090',
        'KIT',
        142.00,
        219.90,
        199.90,
        249.90,
        35.00,
        5,
        80,
        20,
        150,
        'B02-C1',
        1800,
        5.20,
        12.00,
        15.00,
        'Indicado para SUVs e sedans premium.',
        1
    ),
    (
        'VEL-004',
        'Vela de ignicao iridium',
        'Vela iridium de longa duracao com tecnologia laser escalonada.',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-IGN'),
        (SELECT Id FROM est_pecas_unidades WHERE Sigla = 'CX'),
        (SELECT Id FROM est_pecas_marcas WHERE Nome = 'NGK'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0003'),
        'IFR6T11',
        '7891234560404',
        '85111000',
        'CX',
        72.30,
        119.00,
        109.00,
        139.00,
        32.00,
        3,
        150,
        40,
        220,
        'C03-D4',
        320,
        9.00,
        7.50,
        7.50,
        'Caixa com 4 unidades.',
        1
    ),
    (
        'ARF-112',
        'Filtro de ar cabine com carvao ativado',
        'Elemento filtrante com tripla camada e neutralizacao de odores.',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-FILTROS'),
        (SELECT Id FROM est_pecas_unidades WHERE Sigla = 'UN'),
        (SELECT Id FROM est_pecas_marcas WHERE Nome = 'Mahle'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'LAK-888',
        '7891234560112',
        '84213100',
        'UN',
        32.10,
        55.75,
        48.00,
        69.90,
        30.00,
        6,
        60,
        15,
        120,
        'A02-A5',
        260,
        3.50,
        21.00,
        21.00,
        'Compatibilidade com SUVs compactos nacionais.',
        1
    ),
    (
        'ACM-550',
        'Compressor de ar condicionado 12v',
        'Compressor rotativo blindado com oleo PAG46 incluso.',
        (SELECT Id FROM est_pecas_categorias WHERE Codigo = 'CAT-COMP'),
        (SELECT Id FROM est_pecas_unidades WHERE Sigla = 'UN'),
        (SELECT Id FROM est_pecas_marcas WHERE Nome = 'Denso'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        '10S17C-PAG46',
        '7891234560550',
        '84143011',
        'UN',
        820.00,
        980.00,
        920.00,
        1150.00,
        32.00,
        10,
        10,
        2,
        30,
        'D01-E1',
        7800,
        18.50,
        24.00,
        32.00,
        'Inclui selo de garantia e manual tecnico.',
        1
    )
ON DUPLICATE KEY UPDATE
    Descricao = VALUES(Descricao),
    Descricao_Detalhada = VALUES(Descricao_Detalhada),
    Categoria_Id = VALUES(Categoria_Id),
    Unidade_Id = VALUES(Unidade_Id),
    Marca_Id = VALUES(Marca_Id),
    Fornecedor_Id = VALUES(Fornecedor_Id),
    Numero_Fabricante = VALUES(Numero_Fabricante),
    Codigo_Barras = VALUES(Codigo_Barras),
    Ncm = VALUES(Ncm),
    Unidade_Compra = VALUES(Unidade_Compra),
    Preco_Custo = VALUES(Preco_Custo),
    Preco_Unitario = VALUES(Preco_Unitario),
    Preco_Minimo = VALUES(Preco_Minimo),
    Preco_Maximo = VALUES(Preco_Maximo),
    Margem_Sugerida = VALUES(Margem_Sugerida),
    Prazo_Reposicao = VALUES(Prazo_Reposicao),
    Quantidade = VALUES(Quantidade),
    Estoque_Minimo = VALUES(Estoque_Minimo),
    Estoque_Maximo = VALUES(Estoque_Maximo),
    Localizacao_Estoque = VALUES(Localizacao_Estoque),
    Peso_Gramas = VALUES(Peso_Gramas),
    Altura_CM = VALUES(Altura_CM),
    Largura_CM = VALUES(Largura_CM),
    Comprimento_CM = VALUES(Comprimento_CM),
    Observacoes = VALUES(Observacoes),
    Ativo = VALUES(Ativo),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_fornecedores (
    Peca_Id, Fornecedor_Id, Codigo_Fornecedor, Prazo_Entrega_Dias, Preco_Custo,
    Moeda, Lote_Minimo, Condicao_Pagamento, Status, Observacoes
)
VALUES
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'FLT-001'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0001'),
        'AUTO-FLT-001',
        5,
        24.50,
        'BRL',
        20,
        'Boleto 28 dias',
        'ATIVO',
        'Remessa semanal programada.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'FLT-001'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0004'),
        'MBR-FLT-90',
        7,
        25.80,
        'BRL',
        15,
        'Boleto 21 dias',
        'ATIVO',
        'Lead time maior, usado como backup.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'PST-020'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0002'),
        'PP-BPX-200',
        4,
        142.00,
        'BRL',
        10,
        'Pix 2% antecipado',
        'ATIVO',
        'Disponibilidade imediata para linha premium.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'ACM-550'),
        (SELECT Id FROM cad_fornecedores WHERE Codigo = 'FORN-0005'),
        'AC-550',
        6,
        820.00,
        'BRL',
        2,
        'Boleto 21 dias',
        'ATIVO',
        'Compressor com garantia estendida.'
    )
ON DUPLICATE KEY UPDATE
    Prazo_Entrega_Dias = VALUES(Prazo_Entrega_Dias),
    Preco_Custo = VALUES(Preco_Custo),
    Moeda = VALUES(Moeda),
    Lote_Minimo = VALUES(Lote_Minimo),
    Condicao_Pagamento = VALUES(Condicao_Pagamento),
    Status = VALUES(Status),
    Observacoes = VALUES(Observacoes),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_aplicacoes (
    Peca_Id, Modelo_Veiculo_Id, Ano_Inicio, Ano_Fim, Observacao
)
VALUES
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'FLT-001'),
        (SELECT Id FROM cad_veiculos_modelos WHERE Nome = 'Corolla XEi'),
        2016,
        NULL,
        'Motores 2.0 flex com codigo 3ZR-FBE.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'PST-020'),
        (SELECT Id FROM cad_veiculos_modelos WHERE Nome = 'T-Cross Highline'),
        2019,
        NULL,
        'Requer disco ventilado de 288mm.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'ARF-112'),
        (SELECT Id FROM cad_veiculos_modelos WHERE Nome = 'Onix LTZ'),
        2020,
        NULL,
        'Cabine com filtro anti polen.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'ACM-550'),
        (SELECT Id FROM cad_veiculos_modelos WHERE Nome = 'Ranger XLS'),
        2017,
        NULL,
        'Compatibilidade com motor 3.2 diesel.'
    )
ON DUPLICATE KEY UPDATE
    Ano_Inicio = VALUES(Ano_Inicio),
    Ano_Fim = VALUES(Ano_Fim),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_precos_historico (
    Peca_Id, Data_Referencia, Preco_Custo, Preco_Venda, Moeda, Fonte, Observacao
)
VALUES
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'FLT-001'),
        DATE_SUB(CURDATE(), INTERVAL 90 DAY),
        23.40,
        36.90,
        'BRL',
        'Tabela Autosul 2024Q2',
        'Reajuste aplicado em julho de 2024.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'PST-020'),
        DATE_SUB(CURDATE(), INTERVAL 60 DAY),
        138.00,
        212.00,
        'BRL',
        'Premium Parts Agosto 2024',
        'Reajuste devido ao aumento de materia prima.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'ACM-550'),
        DATE_SUB(CURDATE(), INTERVAL 120 DAY),
        798.00,
        950.00,
        'BRL',
        'AutoClima Maio 2024',
        'Tabela antes do reajuste anual.'
    )
ON DUPLICATE KEY UPDATE
    Preco_Custo = VALUES(Preco_Custo),
    Preco_Venda = VALUES(Preco_Venda),
    Moeda = VALUES(Moeda),
    Fonte = VALUES(Fonte),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO est_pecas_arquivos (
    Peca_Id, Tipo, Nome_Arquivo, Url, Observacao
)
VALUES
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'FLT-001'),
        'Ficha Tecnica',
        'ficha-flt-001.pdf',
        'https://cdn.autosul.com.br/fichas/ficha-flt-001.pdf',
        'Instrucoes de torque e substituicao.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'PST-020'),
        'Manual de Instalacao',
        'manual-pst-020.pdf',
        'https://premium-parts.example.com/manual/manual-pst-020.pdf',
        'Requer assentamento em 300km.'
    ),
    (
        (SELECT Id FROM est_pecas WHERE Codigo = 'ACM-550'),
        'Explodido Tecnico',
        'explodido-acm-550.png',
        'https://autoclima.com.br/manual/explodidos/explodido-acm-550.png',
        'Diagrama de montagem detalhado.'
    )
ON DUPLICATE KEY UPDATE
    Url = VALUES(Url),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);
