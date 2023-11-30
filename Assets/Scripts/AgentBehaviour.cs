using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AgentBehaviour : MonoBehaviour
    {
        Animator animator;
        private AgentState state;
        private Grid grid;
        private Cell currentCell;

        private Transform transformBeforeSleep;

        private const int BASE_SATIETY = 3600;
        private const int BASE_ENERGY = 10000;

        public int satiety = BASE_SATIETY;
        public int energy = BASE_ENERGY;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            //isWalkingHash = animator. TODO
            state = AgentState.IDLE;
            grid = GameObject.FindGameObjectWithTag("GRID").GetComponent<Grid>();
            //foreach(Cell cellGO in grid.GetCells())
            //{
            //    if(cellGO.content==)
            //}
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("a")) SetState(AgentState.MOVING);
            if (Input.GetKey("z")) SetState(AgentState.INTERACTING);
            if (Input.GetKey("e")) SetState(AgentState.STUCK);
            if (Input.GetKey("r")) SetState(AgentState.SLEEPING);
        }

        public void SetTarget(Cell cell)
        {
            if (cell != currentCell)
            {
                OnMoveBegin();
            }
        }
        public void InteractWith(Cell cell)
        {
            OnInteractionBegin(cell);
        }
        public void Sleep()
        {
            OnSleepingBegin();
        }

        private void OnMoveBegin()
        {
            SetState(AgentState.MOVING);
        }
        private void OnInteractionBegin(Cell cell)
        {
            SetState(AgentState.INTERACTING);
        }
        private void OnStuckBegin()
        {
            SetState(AgentState.STUCK);
        }
        private void OnSleepingBegin()
        {
            //TODO change position
            transformBeforeSleep = transform;
            SetState(AgentState.SLEEPING);
            Invoke(nameof(ResumeAfterSleep), 10);
        }

        private void ResumeAfterSleep()
        {
            SetState(AgentState.IDLE);
            transform.position = transformBeforeSleep.position;
            transform.rotation = transformBeforeSleep.rotation;
        }

        public void SetState(AgentState state)
        {
            this.state = state;
            switch (this.state)
            {
                case AgentState.IDLE:
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isGrabing", false);
                    animator.SetBool("isStuck", false);
                    animator.SetBool("isSleeping", false);
                    break;
                case AgentState.STUCK:
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isGrabing", false);
                    animator.SetBool("isStuck", true);
                    animator.SetBool("isSleeping", false);
                    break;
                case AgentState.MOVING:
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isGrabing", false);
                    animator.SetBool("isStuck", false);
                    animator.SetBool("isSleeping", false);
                    break;
                case AgentState.INTERACTING:
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isGrabing", true);
                    animator.SetBool("isStuck", false);
                    animator.SetBool("isSleeping", false);
                    break;
                case AgentState.SLEEPING:
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isGrabing", false);
                    animator.SetBool("isStuck", false);
                    animator.SetBool("isSleeping", true);
                    break;
            }
        }
    }
}