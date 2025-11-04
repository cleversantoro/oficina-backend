USE oficina_db;

SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE fin_nfes;
TRUNCATE TABLE fin_pagamentos;
TRUNCATE TABLE os_itens;
TRUNCATE TABLE os_ordens;
TRUNCATE TABLE est_pecas_arquivos;
TRUNCATE TABLE est_pecas_precos_historico;
TRUNCATE TABLE est_pecas_aplicacoes;
TRUNCATE TABLE est_pecas_fornecedores;
TRUNCATE TABLE est_movimentacoes;
TRUNCATE TABLE est_pecas;
TRUNCATE TABLE est_pecas_marcas;
TRUNCATE TABLE est_pecas_unidades;
TRUNCATE TABLE est_pecas_categorias;
TRUNCATE TABLE cad_veiculos;
TRUNCATE TABLE cad_veiculos_modelos;
TRUNCATE TABLE cad_veiculos_marcas;
TRUNCATE TABLE cad_clientes_anexos;
TRUNCATE TABLE cad_clientes_lgpd_consentimentos;
TRUNCATE TABLE cad_clientes_financeiro;
TRUNCATE TABLE cad_clientes_indicacoes;
TRUNCATE TABLE cad_clientes_contatos;
TRUNCATE TABLE cad_clientes_enderecos;
TRUNCATE TABLE cad_clientes_pf;
TRUNCATE TABLE cad_clientes_pj;
TRUNCATE TABLE cad_fornecedores_avaliacoes;
TRUNCATE TABLE cad_fornecedores_certificacoes;
TRUNCATE TABLE cad_fornecedores_documentos;
TRUNCATE TABLE cad_fornecedores_bancos;
TRUNCATE TABLE cad_fornecedores_representantes;
TRUNCATE TABLE cad_fornecedores_contatos;
TRUNCATE TABLE cad_fornecedores_enderecos;
TRUNCATE TABLE cad_fornecedores_segmentos_rel;
TRUNCATE TABLE cad_fornecedores;
TRUNCATE TABLE cad_fornecedores_segmentos;
TRUNCATE TABLE cad_mecanicos;
TRUNCATE TABLE cad_clientes;
TRUNCATE TABLE cad_clientes_origens;

SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO cad_clientes_origens (Id, Nome, Descricao, Created_At, Updated_At) VALUES
(1, 'Campanha Digital', 'Leads captados por anúncios on-line', NOW(), NULL),
(2, 'Indicação', 'Clientes indicados por parceiros', NOW(), NULL),
(3, 'Walk-in', 'Atendimento presencial na oficina', NOW(), NULL),
(4, 'Telefone', 'Contato via central telefônica', NOW(), NULL),
(5, 'Aplicativo', 'Cadastro realizado no app', NOW(), NULL);

INSERT INTO cad_clientes (Id, Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES
(101, 20241013001, 'Mariana Souza', 'Mariana Souza', '12345678901', 1, 1, 0, 'Cliente recém-captada via campanha digital', 1, '(11) 91234-0001', 'mariana.souza@example.com', 1, NOW(), NULL),
(102, 20241013002, 'Carlos Barbosa', 'Carlos Barbosa', '98765432100', 1, 1, 1, 'Prefere agendamentos aos sábados', 2, '(11) 98888-2222', 'carlos.barbosa@example.com', 2, NOW(), NULL),
(103, 20241013003, 'Auto Peças Horizonte LTDA', 'Auto Peças Horizonte', '11222333444455', 2, 1, 0, 'Fornecedor eventual de peças', 3, '(11) 3456-7890', 'contato@autohorizonte.com.br', 3, NOW(), NULL),
(104, 20241013004, 'Luciana Prado', 'Luciana Prado', '45678912300', 1, 2, 0, 'Cliente aguardando resposta de orçamento', 4, '(11) 95555-7777', 'luciana.prado@example.com', 4, NOW(), NULL),
(105, 20241013005, 'TechFleet Serviços LTDA', 'TechFleet', '55443322110088', 2, 1, 1, 'Contrato de manutenção de frota', 5, '(11) 4000-8888', 'suporte@techfleet.com.br', 5, NOW(), NULL);

INSERT INTO cad_clientes_pf (Id, Cliente_Id, Cpf, Rg, Data_Nascimento, Genero, Estado_Civil, Profissao, Created_At, Updated_At)
VALUES
(201, 101, '12345678901', '45.123.987-1', '1990-05-12', 'Feminino', 'Solteira', 'Designer', NOW(), NULL),
(202, 102, '98765432100', '12.987.654-2', '1985-03-22', 'Masculino', 'Casado', 'Analista de Sistemas', NOW(), NULL),
(203, 104, '45678912300', '98.321.456-9', '1993-11-01', 'Feminino', 'Divorciada', 'Administradora', NOW(), NULL),
(204, 0, '00000000000', NULL, NULL, NULL, NULL, NULL, NOW(), NULL),
(205, 0, '11111111111', NULL, NULL, NULL, NULL, NULL, NOW(), NULL);

INSERT INTO cad_clientes_pj (Id, Cliente_Id, Cnpj, Razao_Social, Nome_Fantasia, Inscricao_Estadual, Inscricao_Municipal, Responsavel, Created_At, Updated_At)
VALUES
(301, 103, '11222333444455', 'Auto Peças Horizonte LTDA', 'Auto Horizonte', '112.223.334.444', '12.345.678', 'Paulo Henrique', NOW(), NULL),
(302, 105, '55443322110088', 'TechFleet Serviços LTDA', 'TechFleet', '554.433.221.100', '98.765.432', 'Renata Lima', NOW(), NULL),
(303, 0, '00000000000000', 'Placeholder Indústria', 'Indústria Demo', NULL, NULL, NULL, NOW(), NULL),
(304, 0, '11111111000111', 'Fictícia Logística', 'Fictícia Logística', NULL, NULL, NULL, NOW(), NULL),
(305, 0, '22222222000122', 'Manutenção XPTO', 'Manutenção XPTO', NULL, NULL, NULL, NOW(), NULL);

INSERT INTO cad_clientes_enderecos (Id, Cliente_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal, Created_At, Updated_At)
VALUES
(401, 101, 1, '01310-100', 'Av. Paulista', '1000', 'Bela Vista', 'São Paulo', 'SP', 'Brasil', 'Apto 101', 1, NOW(), NULL),
(402, 102, 1, '04001-001', 'Rua Vergueiro', '250', 'Paraíso', 'São Paulo', 'SP', 'Brasil', NULL, 1, NOW(), NULL),
(403, 103, 2, '05005-050', 'Rua das Oficinas', '500', 'Lapa', 'São Paulo', 'SP', 'Brasil', 'Galpão B', 1, NOW(), NULL),
(404, 104, 3, '04567-020', 'Av. Faria Lima', '1500', 'Itaim Bibi', 'São Paulo', 'SP', 'Brasil', 'Conj. 1207', 1, NOW(), NULL),
(405, 105, 2, '06000-020', 'Rua do Progresso', '900', 'Centro', 'Osasco', 'SP', 'Brasil', 'Sala 05', 1, NOW(), NULL);

INSERT INTO cad_clientes_contatos (Id, Cliente_Id, Tipo, Valor, Principal, Observacao, Created_At, Updated_At)
VALUES
(501, 101, 2, '(11) 91234-0001', 1, 'WhatsApp', NOW(), NULL),
(502, 102, 3, 'carlos.barbosa@example.com', 1, 'E-mail preferencial', NOW(), NULL),
(503, 103, 1, '(11) 3456-7890', 1, 'Telefone comercial', NOW(), NULL),
(504, 104, 2, '(11) 95555-7777', 1, 'WhatsApp', NOW(), NULL),
(505, 105, 3, 'suporte@techfleet.com.br', 1, 'Chamados da frota', NOW(), NULL);

INSERT INTO cad_clientes_indicacoes (Id, Cliente_Id, Indicador_Nome, Indicador_Telefone, Observacao, Created_At, Updated_At)
VALUES
(601, 101, 'Juliana Ramos', '(11) 98888-0000', 'Amiga de infância', NOW(), NULL),
(602, 102, 'Pedro Oliveira', '(11) 97777-3333', 'Colega de trabalho', NOW(), NULL),
(603, 103, 'Ricardo Auto Center', '(11) 3666-4444', 'Parceiro comercial', NOW(), NULL),
(604, 104, 'Fernanda Dias', '(11) 96666-2222', 'Indicação via telefone', NOW(), NULL),
(605, 105, 'FleetPlus', '(11) 4000-9999', 'Parceiro de negócios', NOW(), NULL);

INSERT INTO cad_clientes_lgpd_consentimentos (Id, Cliente_Id, Tipo, Aceito, Data, Valido_Ate, Observacoes, Canal, Created_At, Updated_At)
VALUES
(701, 101, 1, 1, NOW(), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Aceitou receber promoções', 'API', NOW(), NULL),
(702, 102, 2, 1, NOW(), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Consentiu com compartilhamento de dados de serviço', 'API', NOW(), NULL),
(703, 103, 3, 1, NOW(), DATE_ADD(NOW(), INTERVAL 2 YEAR), 'Aprova comunicações operacionais', 'Portal', NOW(), NULL),
(704, 104, 1, 0, NOW(), NULL, 'Não deseja receber marketing', 'Telefone', NOW(), NULL),
(705, 105, 3, 1, NOW(), DATE_ADD(NOW(), INTERVAL 3 YEAR), 'Notificações sobre contratos', 'Aplicativo', NOW(), NULL);

INSERT INTO cad_clientes_financeiro (Id, Cliente_Id, Limite_Credito, Prazo_Pagamento, Bloqueado, Observacoes, Created_At, Updated_At)
VALUES
(801, 101, 5000.00, 15, 0, 'Limite padrão PF', NOW(), NULL),
(802, 102, 8000.00, 20, 0, 'Cliente VIP', NOW(), NULL),
(803, 103, 15000.00, 30, 0, 'Conforme contrato de fornecimento', NOW(), NULL),
(804, 104, 3000.00, 10, 0, 'Liberado após análise', NOW(), NULL),
(805, 105, 25000.00, 45, 0, 'Contrato corporativo', NOW(), NULL);

INSERT INTO cad_clientes_anexos (Id, Cliente_Id, Nome, Tipo, Url, Observacao, Created_At, Updated_At)
VALUES
(901, 101, 'RG_Mariana.pdf', 'application/pdf', 'https://storage.oficina.com/docs/RG_Mariana.pdf', 'Documento de identificação', NOW(), NULL),
(902, 102, 'CNH_Carlos.jpg', 'image/jpeg', 'https://storage.oficina.com/docs/CNH_Carlos.jpg', 'CNH digitalizada', NOW(), NULL),
(903, 103, 'Contrato_Parceria.pdf', 'application/pdf', 'https://storage.oficina.com/docs/Contrato_Parceria.pdf', 'Contrato de fornecimento', NOW(), NULL),
(904, 104, 'Orcamento_2024.xlsx', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'https://storage.oficina.com/docs/Orcamento_2024.xlsx', 'Orçamento aprovado', NOW(), NULL),
(905, 105, 'Frota_TechFleet.csv', 'text/csv', 'https://storage.oficina.com/docs/Frota_TechFleet.csv', 'Relação de veículos', NOW(), NULL);

INSERT INTO cad_mecanicos (Id, Nome, Especialidade, Ativo, Created_At, Updated_At)
VALUES
(1001, 'Rogério Lima', 'Motor e transmissão', 1, NOW(), NULL),
(1002, 'Carla Mendes', 'Elétrica automotiva', 1, NOW(), NULL),
(1003, 'Paulo Ferreira', 'Suspensão e freios', 1, NOW(), NULL),
(1004, 'Ana Beatriz', 'Diagnóstico eletrônico', 1, NOW(), NULL),
(1005, 'Fábio Santos', 'Ar-condicionado', 1, NOW(), NULL);

INSERT INTO cad_fornecedores_segmentos (Id, Codigo, Nome, Descricao, Ativo, Created_At, Updated_At)
VALUES
(2101, 'AUTOPE', 'Autopecas', 'Distribuidores de componentes mecanicos', 1, NOW(), NULL),
(2102, 'LUBRI', 'Lubrificantes', 'Oleos e fluidos automotivos', 1, NOW(), NULL),
(2103, 'ELETR', 'Eletrica e Eletronica', 'Sensores, modulos e sistemas de ignicao', 1, NOW(), NULL),
(2104, 'CLIMA', 'Climatizacao', 'Componentes de HVAC automotivo', 1, NOW(), NULL),
(2105, 'SUSP', 'Suspensao e Freios', 'Componentes de seguranca e suspensao', 1, NOW(), NULL);

INSERT INTO cad_fornecedores (
    Id, Codigo, Tipo, Razao_Social, Nome_Fantasia, Documento, Inscricao_Estadual,
    Inscricao_Municipal, Segmento_Principal_Id, Website, Email, Telefone_Principal,
    Status, Condicao_Pagamento_Padrao, Prazo_Entrega_Medio, Nota_Media, Observacoes,
    Prazo_Garantia_Padrao, Termos_Negociados, Atendimento_Personalizado, Retirada_Local,
    Rating_Logistica, Rating_Qualidade, Created_At, Updated_At
)
VALUES
(1101, 'FORN-0001', 'PJ', 'Distribuidora Autosul LTDA', 'Autosul', '00987654321098', '112223334445', '123456789',
 (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'AUTOPE'), 'https://www.autosul.com.br', 'comercial@autosul.com.br',
 '(11) 4002-8922', 'ATIVO', 'Boleto 28 dias', 5, 4.60, 'Linha premium de filtros, lubrificantes e componentes do motor.',
 'Garantia de 6 meses para itens mecanicos conforme fabricante.', 'Desconto de 3% para pagamento antecipado.', 1, 1, 4.70, 4.80, NOW(), NULL),
(1102, 'FORN-0002', 'PJ', 'Pecas Premium SA', 'Premium Parts', '00112233445566', '998877665544', '445566778',
 (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'SUSP'), 'https://fornecedor-premium.example.com', 'vendas@pecaspremium.com',
 '(11) 3254-7788', 'ATIVO', 'Pix imediato com 5% off ou boleto 21 dias', 4, 4.50, 'Especialista em freios e suspensao de alta performance.',
 'Garantia de 12 meses para itens homologados.', 'Entrega consignada mediante contrato.', 1, 0, 4.60, 4.75, NOW(), NULL),
(1103, 'FORN-0003', 'PJ', 'Logparts Comercio e Logistica LTDA', 'Logparts', '00334455667788', '221133445566', '556677889',
 (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'ELETR'), 'https://www.logparts.com.br', 'suporte@logparts.com.br',
 '(11) 2555-9900', 'ATIVO', 'Faturamento quinzenal com limite de credito ativo.', 3, 4.40, 'Distribuidor com centros em SP e PR.',
 'Garantia de 1 ano para modulos e componentes.', 'Frete incluso em pedidos acima de R$ 2.000,00.', 1, 1, 4.55, 4.60, NOW(), NULL),
(1104, 'FORN-0004', 'PJ', 'Motores Brasil Comercial LTDA', 'Motores Brasil', '00778899001122', '445566771122', '112244335',
 (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'AUTOPE'), 'https://www.motoresbr.com', 'atendimento@motoresbr.com',
 '(11) 3090-4020', 'ATIVO', 'Boleto 30 dias com limite sob consulta.', 7, 4.30, 'Kits de revisao e componentes pesados.',
 'Garantia ate 18 meses em linha premium.', 'Consignacao para itens de alta rotatividade.', 0, 1, 4.30, 4.55, NOW(), NULL),
(1105, 'FORN-0005', 'PJ', 'AutoClima Servicos Integrados LTDA', 'AutoClima', '00556677889900', '556677889900', '778899001',
 (SELECT Id FROM cad_fornecedores_segmentos WHERE Codigo = 'CLIMA'), 'https://www.autoclima.com.br', 'contato@autoclima.com.br',
 '(11) 2789-4545', 'ATIVO', 'Pagamento em 21 dias com desconto progressivo.', 6, 4.20, 'Solucoes completas em climatizacao automotiva.',
 'Garantia de 1 ano com rede credenciada.', 'SLA emergencial de entrega em ate 12h em SP.', 1, 0, 4.40, 4.65, NOW(), NULL);

INSERT INTO cad_veiculos_marcas (Id, Nome, Pais, Created_At, Updated_At)
VALUES
(1201, 'Toyota', 'Japão', NOW(), NULL),
(1202, 'Chevrolet', 'Estados Unidos', NOW(), NULL),
(1203, 'Volkswagen', 'Alemanha', NOW(), NULL),
(1204, 'Fiat', 'Itália', NOW(), NULL),
(1205, 'Ford', 'Estados Unidos', NOW(), NULL);

INSERT INTO cad_veiculos_modelos (Id, Marca_Id, Nome, Ano_Inicio, Ano_Fim, Created_At, Updated_At)
VALUES
(1301, 1201, 'Corolla XEi', 2019, NULL, NOW(), NULL),
(1302, 1202, 'Onix LTZ', 2018, NULL, NOW(), NULL),
(1303, 1203, 'T-Cross Highline', 2020, NULL, NOW(), NULL),
(1304, 1204, 'Cronos Precision', 2019, NULL, NOW(), NULL),
(1305, 1205, 'Ranger XLS', 2017, NULL, NOW(), NULL);

INSERT INTO cad_veiculos (Id, Cliente_Id, Placa, Marca, Modelo_Id, Ano, Cor, Chassi, Renavam, Combustivel, Observacao, Principal, Created_At, Updated_At)
VALUES
(1401, 101, 'ABC1D23', 'Toyota', 1301, 2021, 'Prata', '9BRBLW12345678901', '01234567890', 'Gasolina', 'Veículo principal', 1, NOW(), NULL),
(1402, 102, 'EFG4H56', 'Chevrolet', 1302, 2020, 'Preto', '9BGBCW23456789012', '11223344556', 'Flex', 'Utilitário urbano', 1, NOW(), NULL),
(1403, 103, 'IJK7L89', 'Volkswagen', 1303, 2022, 'Branco', '9BWZZZ377VT004251', '22334455667', 'Gasolina', 'Veículo de testes', 1, NOW(), NULL),
(1404, 104, 'MNO0P12', 'Fiat', 1304, 2019, 'Vermelho', '8APZZZ15678945012', NULL, 'Flex', 'Uso particular', 1, NOW(), NULL),
(1405, 105, 'QRS3T45', 'Ford', 1305, 2021, 'Azul', '9BFZXXE6549873210', '33445566778', 'Diesel', 'Frota TechFleet', 1, NOW(), NULL);

INSERT INTO est_pecas_categorias (Id, Codigo, Nome, Descricao, Categoria_Pai_Id, Ativo, Created_At, Updated_At)
VALUES
(501, 'CAT-ENG', 'Componentes de Motor', 'Itens aplicados ao grupo motriz', NULL, 1, NOW(), NULL),
(502, 'CAT-FRE', 'Sistema de Freio', 'Pastilhas, discos e hidraulico', NULL, 1, NOW(), NULL),
(503, 'CAT-CLM', 'Climatizacao', 'Componentes de ar condicionado e ventilacao', NULL, 1, NOW(), NULL),
(504, 'CAT-FILTROS', 'Filtros e Elementos', 'Filtros de oleo, ar e combustivel', 501, 1, NOW(), NULL),
(505, 'CAT-IGN', 'Ignicao', 'Velas, bobinas e cabos de velas', 501, 1, NOW(), NULL),
(506, 'CAT-COMP', 'Compressores e HVAC', 'Compressores, evaporadores e filtros secadores', 503, 1, NOW(), NULL);

INSERT INTO est_pecas_unidades (Id, Sigla, Descricao, Observacao, Created_At, Updated_At)
VALUES
(601, 'UN', 'Unidade', 'Unidade individual', NOW(), NULL),
(602, 'CX', 'Caixa', 'Embalagem com multiplas unidades', NOW(), NULL),
(603, 'KIT', 'Kit', 'Conjunto com componentes diversos', NOW(), NULL);

INSERT INTO est_pecas_marcas (Id, Nome, Pais, Website, Created_At, Updated_At)
VALUES
(701, 'Mahle', 'Alemanha', 'https://www.mahle-aftermarket.com', NOW(), NULL),
(702, 'Bosch', 'Alemanha', 'https://www.boschautoparts.com', NOW(), NULL),
(703, 'NGK', 'Japao', 'https://www.ngkntk.com', NOW(), NULL),
(704, 'Denso', 'Japao', 'https://www.denso.com', NOW(), NULL);

INSERT INTO est_pecas (
    Id, Codigo, Descricao, Descricao_Detalhada, Categoria_Id, Unidade_Id, Marca_Id, Fornecedor_Id,
    Numero_Fabricante, Codigo_Barras, Ncm, Unidade_Compra, Preco_Custo, Preco_Unitario,
    Preco_Minimo, Preco_Maximo, Margem_Sugerida, Prazo_Reposicao, Quantidade, Estoque_Minimo,
    Estoque_Maximo, Localizacao_Estoque, Peso_Gramas, Altura_CM, Largura_CM, Comprimento_CM,
    Observacoes, Ativo, Created_At, Updated_At
)
VALUES
(1501, 'FLT-001', 'Filtro de oleo sintetico', 'Filtro linha leve com elemento em celulose reforcada e valvula anti-retorno.', 504, 601, 701, 1101, 'OC-90', '7891234560011', '84212390', 'UN', 24.50, 39.90, 35.00, 49.90, 35.00, 7, 120, 30, 250, 'A01-B2', 420, 9.50, 8.00, 8.00, 'Aplicacao em motores 1.0 a 2.0 flex.', 1, NOW(), NULL),
(1502, 'PST-020', 'Pastilha de freio dianteira ceramica', 'Pastilha ceramica para uso severo com sensor de desgaste integrado.', 502, 603, 702, 1102, 'BPX-200', '7891234560202', '87083090', 'KIT', 142.00, 219.90, 199.90, 249.90, 35.00, 5, 80, 20, 150, 'B02-C1', 1800, 5.20, 12.00, 15.00, 'Indicado para SUVs e sedans premium.', 1, NOW(), NULL),
(1503, 'VEL-004', 'Vela de ignicao iridium', 'Vela iridium de longa duracao com tecnologia laser escalonada.', 505, 602, 703, 1103, 'IFR6T11', '7891234560404', '85111000', 'CX', 72.30, 119.00, 109.00, 139.00, 32.00, 3, 150, 40, 220, 'C03-D4', 320, 9.00, 7.50, 7.50, 'Caixa com 4 unidades.', 1, NOW(), NULL),
(1504, 'ARF-112', 'Filtro de ar cabine com carvao ativado', 'Elemento filtrante com tripla camada e neutralizacao de odores.', 504, 601, 701, 1101, 'LAK-888', '7891234560112', '84213100', 'UN', 32.10, 55.75, 48.00, 69.90, 30.00, 6, 60, 15, 120, 'A02-A5', 260, 3.50, 21.00, 21.00, 'Compatibilidade com SUVs compactos nacionais.', 1, NOW(), NULL),
(1505, 'ACM-550', 'Compressor de ar condicionado 12v', 'Compressor rotativo blindado com oleo PAG46 incluso.', 506, 601, 704, 1105, '10S17C-PAG46', '7891234560550', '84143011', 'UN', 820.00, 980.00, 920.00, 1150.00, 32.00, 10, 10, 2, 30, 'D01-E1', 7800, 18.50, 24.00, 32.00, 'Inclui selo de garantia e manual tecnico.', 1, NOW(), NULL);

INSERT INTO est_pecas_fornecedores (Id, Peca_Id, Fornecedor_Id, Codigo_Fornecedor, Prazo_Entrega_Dias, Preco_Custo, Moeda, Lote_Minimo, Condicao_Pagamento, Status, Observacoes, Created_At, Updated_At)
VALUES
(801, 1501, 1101, 'AUTO-FLT-001', 5, 24.50, 'BRL', 20, 'Boleto 28 dias', 'ATIVO', 'Remessa semanal programada.', NOW(), NULL),
(802, 1501, 1104, 'MBR-FLT-90', 7, 25.80, 'BRL', 15, 'Boleto 21 dias', 'ATIVO', 'Lead time maior, usado como backup.', NOW(), NULL),
(803, 1502, 1102, 'PP-BPX-200', 4, 142.00, 'BRL', 10, 'Pix 2% antecipado', 'ATIVO', 'Disponibilidade imediata para linha premium.', NOW(), NULL),
(804, 1505, 1105, 'AC-550', 6, 820.00, 'BRL', 2, 'Boleto 21 dias', 'ATIVO', 'Compressor com garantia estendida.', NOW(), NULL);

INSERT INTO est_pecas_aplicacoes (Id, Peca_Id, Modelo_Veiculo_Id, Ano_Inicio, Ano_Fim, Observacao, Created_At, Updated_At)
VALUES
(901, 1501, 1301, 2016, NULL, 'Motores 2.0 flex com codigo 3ZR-FBE.', NOW(), NULL),
(902, 1502, 1303, 2019, NULL, 'Requer disco ventilado de 288mm.', NOW(), NULL),
(903, 1504, 1302, 2020, NULL, 'Cabine com filtro anti polen.', NOW(), NULL),
(904, 1505, 1305, 2017, NULL, 'Compatibilidade com motor 3.2 diesel.', NOW(), NULL);

INSERT INTO est_pecas_precos_historico (Id, Peca_Id, Data_Referencia, Preco_Custo, Preco_Venda, Moeda, Fonte, Observacao, Created_At, Updated_At)
VALUES
(1001, 1501, DATE_SUB(CURDATE(), INTERVAL 90 DAY), 23.40, 36.90, 'BRL', 'Tabela Autosul 2024Q2', 'Reajuste aplicado em julho de 2024.', NOW(), NULL),
(1002, 1502, DATE_SUB(CURDATE(), INTERVAL 60 DAY), 138.00, 212.00, 'BRL', 'Premium Parts Agosto 2024', 'Reajuste devido ao aumento de materia prima.', NOW(), NULL),
(1003, 1505, DATE_SUB(CURDATE(), INTERVAL 120 DAY), 798.00, 950.00, 'BRL', 'AutoClima Maio 2024', 'Tabela antes do reajuste anual.', NOW(), NULL);

INSERT INTO est_pecas_arquivos (Id, Peca_Id, Tipo, Nome_Arquivo, Url, Observacao, Created_At, Updated_At)
VALUES
(1101, 1501, 'Ficha Tecnica', 'ficha-flt-001.pdf', 'https://cdn.autosul.com.br/fichas/ficha-flt-001.pdf', 'Instrucoes de torque e substituicao.', NOW(), NULL),
(1102, 1502, 'Manual de Instalacao', 'manual-pst-020.pdf', 'https://premium-parts.example.com/manual/manual-pst-020.pdf', 'Requer assentamento em 300km.', NOW(), NULL),
(1103, 1505, 'Explodido Tecnico', 'explodido-acm-550.png', 'https://autoclima.com.br/manual/explodidos/explodido-acm-550.png', 'Diagrama de montagem detalhado.', NOW(), NULL);

INSERT INTO est_movimentacoes (Id, Peca_Id, Quantidade, Tipo, Referencia, Created_At, Updated_At)
VALUES
(1601, 1501, 50, 'ENTRADA', 'Compra lote março', NOW(), NULL),
(1602, 1502, 20, 'SAIDA', 'OS #2024-001', NOW(), NULL),
(1603, 1503, 30, 'ENTRADA', 'Reposição estoque padrão', NOW(), NULL),
(1604, 1504, 15, 'SAIDA', 'Serviço Luciana Prado', NOW(), NULL),
(1605, 1505, 5, 'ENTRADA', 'Compra frota TechFleet', NOW(), NULL);

INSERT INTO os_ordens (Id, Cliente_Id, Mecanico_Id, Descricao_Problema, Status, Data_Abertura, Data_Conclusao, Created_At, Updated_At)
VALUES
(1701, 101, 1001, 'Troca de óleo e filtro', 'CONCLUIDA', DATE_SUB(NOW(), INTERVAL 7 DAY), DATE_SUB(NOW(), INTERVAL 6 DAY), NOW(), NULL),
(1702, 102, 1002, 'Luz de injeção acesa', 'EM_ANDAMENTO', DATE_SUB(NOW(), INTERVAL 3 DAY), NULL, NOW(), NULL),
(1703, 103, 1003, 'Substituição de pastilhas dianteiras', 'CONCLUIDA', DATE_SUB(NOW(), INTERVAL 10 DAY), DATE_SUB(NOW(), INTERVAL 9 DAY), NOW(), NULL),
(1704, 104, 1004, 'Diagnóstico ruído motor', 'EM_ABERTO', NOW(), NULL, NOW(), NULL),
(1705, 105, 1005, 'Revisão preventiva frota', 'CONCLUIDA', DATE_SUB(NOW(), INTERVAL 15 DAY), DATE_SUB(NOW(), INTERVAL 14 DAY), NOW(), NULL);

INSERT INTO os_itens (Id, Ordem_Servico_Id, Peca_Id, Descricao, Quantidade, Valor_Unitario, Created_At, Updated_At)
VALUES
(1801, 1701, 1501, 'Filtro de óleo sintético', 1, 35.90, NOW(), NULL),
(1802, 1702, 1503, 'Vela de ignição iridium', 4, 45.00, NOW(), NULL),
(1803, 1703, 1502, 'Pastilha de freio dianteira', 1, 89.50, NOW(), NULL),
(1804, 1704, 1504, 'Filtro de ar cabine', 1, 55.75, NOW(), NULL),
(1805, 1705, 1505, 'Compressor de ar condicionado', 1, 980.00, NOW(), NULL);

INSERT INTO fin_pagamentos (Id, Ordem_Servico_Id, Meio, Valor, Status, Transacao_Id, Created_At, Updated_At)
VALUES
(1901, 1701, 'PIX', 249.90, 'PAGO', 'TX-20241013-0001', NOW(), NULL),
(1902, 1702, 'CARTAO', 520.00, 'PROCESSANDO', 'TX-20241015-0002', NOW(), NULL),
(1903, 1703, 'BOLETO', 189.50, 'PAGO', 'TX-20241005-0003', NOW(), NULL),
(1904, 1704, 'PIX', 350.00, 'PENDENTE', NULL, NOW(), NULL),
(1905, 1705, 'BOLETO', 3450.00, 'PAGO', 'TX-20240930-0004', NOW(), NULL);

INSERT INTO fin_nfes (Id, Ordem_Servico_Id, Numero, Chave_Acesso, Status, Created_At, Updated_At)
VALUES
(2001, 1701, 'NFE2024-0001', '35241013000000000123550010000000011000000010', 'EMITIDA', NOW(), NULL),
(2002, 1702, 'NFE2024-0002', '35241015000000000123550020000000022000000020', 'PENDENTE', NOW(), NULL),
(2003, 1703, 'NFE2024-0003', '35241005000000000123550030000000033000000030', 'EMITIDA', NOW(), NULL),
(2004, 1704, 'NFE2024-0004', '35241020000000000123550040000000044000000040', 'AGUARDANDO', NOW(), NULL),
(2005, 1705, 'NFE2024-0005', '35240930000000000123550050000000055000000050', 'EMITIDA', NOW(), NULL);


