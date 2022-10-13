#include "NoEntiendo.hpp"
#include "Pacman.hpp"
#include "Fantasma.hpp"
#include "FantasmaAzul.hpp"
#include "FantasmaVerde.hpp"
#include "Interruptor.hpp"
#include "InterruptorReversible.hpp"
#include "InterruptorConTiempo.hpp"


#define FPS 8
#define ESPERA ((int)(((float)1/FPS) * 1000))

#define NUM_FANTASMAS 2
#define NUM_INTERRUPTORES 2

int main()
{
	NoEntiendo::Iniciar();
	
	Tablero *tablero = new Tablero();
	
	Pacman *pacman = new Pacman(tablero);
	Fantasma **fantasmas = new Fantasma*[NUM_FANTASMAS];
	Interruptor **interruptor = new Interruptor*[NUM_INTERRUPTORES];
	
	fantasmas[0] = new FantasmaAzul(pacman, tablero);
	fantasmas[1] = new FantasmaVerde(pacman, tablero);

	interruptor[0] = new InterruptorReversible(pacman, tablero, 7, 10, 11 ,10);
	interruptor[1] = new InterruptorConTiempo(pacman, tablero, 1, 1, 4 ,2);

	
	int ciclo = 0;

	while(NoEntiendo::EstaVentanaAbierta() && !NoEntiendo::EstaTeclaPulsada(TECLA_ESCAPE) && 
	      !pacman->EstaMuerto() && !tablero->TodosCocosComidos())
	{
		// Actualizar
		
		NoEntiendo::Actualizar();		

		
		for(int i = 0; i < NUM_FANTASMAS; i ++)
		{
			fantasmas[i]->Actualizar();
		}

		pacman->Actualizar();

		for(int i = 0; i < NUM_INTERRUPTORES; i ++)
		{
			interruptor[i]->Actualizar();
		}
		
		// Dibujar
		
		NoEntiendo::LimpiarTilemap(1);
		NoEntiendo::LimpiarTilemap(2);
		NoEntiendo::LimpiarTilemap(3);
		
		for(int i = 0; i < NUM_FANTASMAS; i ++)
		{
			fantasmas[i]->Dibujar();
		}		

		pacman->Dibujar();

		for(int i = 0; i < NUM_FANTASMAS; i ++)
		{
			interruptor[i]->Dibujar();
		}
		
		NoEntiendo::Dibujar();
		
		NoEntiendo::Esperar(ESPERA);
		ciclo ++;
		
	}
	
	if(pacman->EstaMuerto())
	{
		printf("Oooh! Te ha matado un fantasma\n");
	}
	else if(tablero->TodosCocosComidos())
	{
		printf("Felicidades! Te has comido todos los cocos\n");
	}
		
	// Liberamos memoria
	
	for(int i = 0; i < NUM_FANTASMAS; i ++)
	{
		delete fantasmas[i];
		fantasmas[i] = NULL;
	}	

	delete pacman;
	pacman = NULL;
	
	delete tablero;
	
	NoEntiendo::Finalizar();

    return 0;
}