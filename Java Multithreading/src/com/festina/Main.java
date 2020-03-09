package com.festina;

import java.util.concurrent.*;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.function.Consumer;
import java.util.function.IntToLongFunction;

public class Main {

    private static AtomicInteger counter = new AtomicInteger(0);
    private static volatile int counter2 = 0;

    public static void main(String[] args) throws InterruptedException, BrokenBarrierException {

        Runnable unsafe = () -> counter2++;
        Runnable safe = () -> counter.incrementAndGet();


        multiThreadingWithCyclicBarrier(10,1000000,unsafe);

        System.out.println(counter2);


    }


    public static void multiThreadingWithCyclicBarrier(int numOfThreads, int proccessed, Runnable update) throws BrokenBarrierException, InterruptedException {
        ExecutorService executorService = Executors.newFixedThreadPool(numOfThreads);
        CyclicBarrier cyclicBarrier = new CyclicBarrier(numOfThreads+1);
        for(int i = 0; i<numOfThreads; i++){
            executorService.execute(new RunnableExample(cyclicBarrier,proccessed/numOfThreads,update));
        }

        cyclicBarrier.await();
        cyclicBarrier.await();

    }


    public static class RunnableExample implements Runnable{

        private CyclicBarrier cyclicBarrier;
        private int limit;
        private Runnable update;

        private RunnableExample(CyclicBarrier cyclicBarrier,int limit, Runnable update){
            this.cyclicBarrier = cyclicBarrier;
            this.limit = limit;
            this.update = update;

        }


        @Override
        public void run() {
            try {
                cyclicBarrier.await();
                for(int i = 0; i<limit;i++)
                    update.run();
                cyclicBarrier.await();

            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (BrokenBarrierException e) {
                e.printStackTrace();
            }
        }
    }








}
