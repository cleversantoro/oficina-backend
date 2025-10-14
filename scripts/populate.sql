USE oficina_db;

SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE fin_nfes;
TRUNCATE TABLE fin_pagamentos;
TRUNCATE TABLE os_itens;
TRUNCATE TABLE os_ordens;
TRUNCATE TABLE est_movimentacoes;
TRUNCATE TABLE est_pecas;
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
TRUNCATE TABLE cad_fornecedores;
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

INSERT INTO cad_fornecedores (Id, Razao_Social, Cnpj, Contato, Created_At, Updated_At)
VALUES
(1101, 'Distribuidora Autosul LTDA', '00987654321098', 'comercial@autosul.com.br', NOW(), NULL),
(1102, 'Peças Premium SA', '00112233445566', 'vendas@pecaspremium.com', NOW(), NULL),
(1103, 'Logparts Comércio', '00334455667788', 'suporte@logparts.com.br', NOW(), NULL),
(1104, 'Motores Brasil', '00778899001122', 'atendimento@motoresbr.com', NOW(), NULL),
(1105, 'AutoClima Serviços', '00556677889900', 'contato@autoclima.com.br', NOW(), NULL);

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

INSERT INTO est_pecas (Id, Codigo, Descricao, Preco_Unitario, Quantidade, Fornecedor_Id, Created_At, Updated_At)
VALUES
(1501, 'FLT-001', 'Filtro de óleo sintético', 35.90, 120, 1101, NOW(), NULL),
(1502, 'PST-020', 'Pastilha de freio dianteira', 89.50, 80, 1102, NOW(), NULL),
(1503, 'VEL-004', 'Vela de ignição iridium', 45.00, 150, 1103, NOW(), NULL),
(1504, 'ARF-112', 'Filtro de ar cabine', 55.75, 60, 1104, NOW(), NULL),
(1505, 'ACM-550', 'Compressor de ar condicionado', 980.00, 10, 1105, NOW(), NULL);

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
