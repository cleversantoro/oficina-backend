# Oficina Backend

## Visao Geral
- API modular em .NET 8 criada para apoiar a operacao de uma oficina mecanica, cobrindo cadastro de clientes e fornecedores, ordens de servico, estoque e financeiro.
- Os modulos compartilham o mesmo banco MySQL e sao expostos via minimal APIs, com documentacao automatica via Swagger UI.
- Cada endpoint aplica regras de negocio com FluentValidation e registra entidades usando Entity Framework Core com Pomelo MySQL provider.
- O projeto inclui scripts SQL para criar e popular o banco, alem de mocks para integracoes externas (gateway de pagamento e emissao de NF-e).

## Arquitetura em Modulos
- `src/Oficina.Api`: host web minimal API, configura CORS, Swagger, health-check e registra os modulos.
- `src/Oficina.BuildingBlocks`: utilitarios compartilhados (`Oficina.SharedKernel` com `Entity`, `ValueObject`, `Result`) e ganchos de observabilidade.
- `src/Oficina.Modules/Cadastro`: dominios de clientes, mecanicos e fornecedores (relacionamentos completos, enderecos, contatos, veiculos, anexos).
- `src/Oficina.Modules/OrdemServico`: ordens de servico e itens associados.
- `src/Oficina.Modules/Estoque`: cadastro de pecas e movimentacoes de estoque com ajuste automatico de quantidade.
- `src/Oficina.Modules/Financeiro`: pagamentos e NF-e com clientes externos simulados.
- `scripts/`: `mysql-schema.sql`, `populate.sql`, `popultatenovo 1.sql` e `seed macanicos.sql` para criar e semear dados de exemplo.

## Tecnologias Principais
- .NET SDK 8.0
- Entity Framework Core 8 + Pomelo.EntityFrameworkCore.MySql
- FluentValidation 11
- Swashbuckle.AspNetCore (Swagger)
- Docker Compose para infraestrutura opcional (MySQL + phpMyAdmin)

## Requisitos
- .NET SDK 8.0 instalado
- MySQL 8.4 (local ou em container)
- Opcional: Docker e Docker Compose para subir o banco rapidamente

## Configurando o Banco de Dados
1. Subir os containers de banco e phpMyAdmin:
   ```bash
   docker compose up -d mysql phpmyadmin
   ```
2. Criar o schema e dados basicos (exige cliente MySQL):
   ```bash
   mysql -h 127.0.0.1 -P 3306 -u root -p < scripts/mysql-schema.sql
   mysql -h 127.0.0.1 -P 3306 -u root -p < scripts/populate.sql
   mysql -h 127.0.0.1 -P 3306 -u root -p < "scripts/popultatenovo 1.sql"
   mysql -h 127.0.0.1 -P 3306 -u root -p < "scripts/seed macanicos.sql"
   ```
3. Usuarios e senhas padrao: `oficina` / `oficina123` (conforme `docker-compose.yml` e `appsettings.json`).

> Ainda nao ha migrations EF Core versionadas; utilize os scripts SQL para provisionar o banco.

## Executando a API
1. Restaurar dependencias:
   ```bash
   dotnet restore
   ```
2. Compilar:
   ```bash
   dotnet build
   ```
3. Executar a API:
   ```bash
   dotnet run --project src/Oficina.Api/Oficina.Api.csproj
   ```
4. Enderecos padrao:
- API: `http://localhost:5134`
- Swagger UI: `http://localhost:5134/swagger`
- Health check: `GET http://localhost:5134/health`

### Variaveis de Ambiente
- `ConnectionStrings__OficinaDb`: sobrescreve a string de conexao (formato `server=...;port=...;database=...;user=...;password=...`).
- `ASPNETCORE_URLS`: altera as portas expostas (ex.: `http://0.0.0.0:8080`).

### Observacoes sobre Containers
- O `docker-compose.yml` provem apenas MySQL e phpMyAdmin.
- O `Dockerfile` raiz foi gerado automaticamente pelo Visual Studio e aponta para projetos antigos; prefira `dotnet run` ou crie uma imagem especifica para `src/Oficina.Api`.

## Endpoints Principais

### Ferramentas Gerais
| Modulo | Metodo | Rota | Descricao |
| --- | --- | --- | --- |
| Geral | GET | `/health` | Retorna status da aplicacao. |
| Geral | GET | `/swagger` | Interface de exploracao da API. |

### Cadastro
| Metodo | Rota | Descricao |
| --- | --- | --- |
| GET | `/cadastro/clientes` | Lista clientes com filtros por nome, status, tipo, origem e VIP. |
| GET | `/cadastro/clientes/{codigo}` | Retorna detalhes completos do cliente (enderecos, contatos, veiculos, anexos). |
| POST | `/cadastro/clientes` | Cria cliente PF ou PJ com validacoes completas de documentos e relacionamentos. |
| PUT | `/cadastro/clientes/{codigo}` | Atualiza dados do cliente, substituindo colecoes relacionadas. |
| DELETE | `/cadastro/clientes/{codigo}` | Remove cliente e relacionamentos dependentes. |
| GET | `/cadastro/mecanicos` | Lista mecanicos com especialidades, contatos e disponibilidade. |
| POST | `/cadastro/mecanicos` | Cria mecanico validando codigo, documento principal e especialidades. |
| GET | `/cadastro/fornecedores` | Lista fornecedores ativos. |
| POST | `/cadastro/fornecedores` | Cadastra fornecedor com informacoes financeiras e contatos. |

### Ordem de Servico
| Metodo | Rota | Descricao |
| --- | --- | --- |
| GET | `/ordens/` | Lista ordens com itens associados. |
| POST | `/ordens/` | Abre nova ordem de servico para cliente/mecanico. |
| POST | `/ordens/{id}/itens` | Inclui item de servico/peca na ordem. |
| PUT | `/ordens/{id}/status` | Atualiza status (ex.: ABERTA, EM_ANDAMENTO, CONCLUIDA) e data de conclusao. |

### Estoque
| Metodo | Rota | Descricao |
| --- | --- | --- |
| GET | `/estoque/pecas` | Lista pecas com saldo atual. |
| POST | `/estoque/pecas` | Cadastra nova peca vinculada a fornecedor. |
| POST | `/estoque/movimentacoes` | Lanca movimentacao de entrada ou saida ajustando o saldo. |

### Financeiro
| Metodo | Rota | Descricao |
| --- | --- | --- |
| POST | `/financeiro/pagamentos` | Processa pagamento (mock) e grava status inicial. |
| PUT | `/financeiro/pagamentos/{id}/status` | Ajusta status manualmente (ex.: PAGO, FALHA). |
| POST | `/financeiro/nfe` | Emite NF-e (mock) com base em pagamentos aprovados da OS. |

## Validacoes e Regras Destacadas
- Clientes PF exigem CPF com 11 digitos e dados de pessoa fisica; clientes PJ exigem CNPJ com 14 digitos e razao social.
- Enderecos, contatos e anexos possuem limites de tamanho e regras de unicidade (ex.: apenas um endereco principal).
- Mecanicos verificam duplicidade de codigo/documento e consistencia das especialidades informadas.
- Movimentacoes de estoque atualizam automaticamente o saldo da peca; movimentacoes de saida sem saldo suficiente retornam erro.
- Pagamentos so geram NF-e para valores aprovados, utilizando `FakePaymentGatewayClient` e `FakeNfeClient`.

## Observabilidade e Configuracoes
- CORS liberado (`AllowAnyOrigin/Method/Header`) para facilitar integracoes com front-ends durante o desenvolvimento.
- `Oficina.BuildingBlocks.Observability.ServiceRegistration` disponibiliza ponto unico para acoplar OpenTelemetry, Serilog etc. (atualmente vazio).

## Scripts SQL
- `scripts/mysql-schema.sql`: estrutura inicial de tabelas (nomes `cad_`, `os_`, `est_`, `fin_`).
- `scripts/populate.sql` e `scripts/popultatenovo 1.sql`: popula dados de clientes, veiculos, fornecedores e cadastros auxiliares.
- `scripts/seed macanicos.sql`: sementes focadas em mecanicos e especialidades.

## Testes
- Ainda nao ha suite automatizada. Recomenda-se adicionar testes de API/integracao cobrindo os principais fluxos de cadastro, estoque e financeiro.

## Proximos Passos Sugeridos
- Automatizar migrations EF Core e alinhar o Dockerfile para o projeto atual.
- Adicionar testes automatizados e pipeline CI.
- Implementar camada de observabilidade (logs estruturados e metrics) usando o gancho existente.
- Revisar validacoes de regras de negocio (saldo minimo, limites de credito, workflow de OS).
