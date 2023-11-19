export interface FichaRpg {
    editing?: boolean;
    idFichaRpg?: number;
    nivel: number;
    antecedencia: string;
    nomeDoJogador: string;
    raca: string;
    alinhamento: string;
    pontosDeExperiencia: number;
    dataCriacao?: number;
    dataAtualizado?: number;
    deletado?: boolean;
    idUsuario: number;
}